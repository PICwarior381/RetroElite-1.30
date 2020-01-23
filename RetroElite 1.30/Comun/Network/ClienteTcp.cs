using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using RetroElite.Comun.Frames.Transporte;
using RetroElite.Otros;

namespace RetroElite.Comun.Network
{
	// Token: 0x02000086 RID: 134
	public class ClienteTcp : IDisposable
	{
		// Token: 0x1700017E RID: 382
		// (get) Token: 0x06000591 RID: 1425 RVA: 0x00025184 File Offset: 0x00023584
		// (set) Token: 0x06000592 RID: 1426 RVA: 0x0002518C File Offset: 0x0002358C
		private Socket socket { get; set; }

		// Token: 0x1700017F RID: 383
		// (get) Token: 0x06000593 RID: 1427 RVA: 0x00025195 File Offset: 0x00023595
		// (set) Token: 0x06000594 RID: 1428 RVA: 0x0002519D File Offset: 0x0002359D
		private byte[] buffer { get; set; }

		// Token: 0x14000026 RID: 38
		// (add) Token: 0x06000595 RID: 1429 RVA: 0x000251A8 File Offset: 0x000235A8
		// (remove) Token: 0x06000596 RID: 1430 RVA: 0x000251E0 File Offset: 0x000235E0
		public event Action<string> paquete_recibido;

		// Token: 0x14000027 RID: 39
		// (add) Token: 0x06000597 RID: 1431 RVA: 0x00025218 File Offset: 0x00023618
		// (remove) Token: 0x06000598 RID: 1432 RVA: 0x00025250 File Offset: 0x00023650
		public event Action<string> paquete_enviado;

		// Token: 0x14000028 RID: 40
		// (add) Token: 0x06000599 RID: 1433 RVA: 0x00025288 File Offset: 0x00023688
		// (remove) Token: 0x0600059A RID: 1434 RVA: 0x000252C0 File Offset: 0x000236C0
		public event Action<string> socket_informacion;

		// Token: 0x0600059B RID: 1435 RVA: 0x000252F5 File Offset: 0x000236F5
		public ClienteTcp(Cuenta _cuenta)
		{
			this.cuenta = _cuenta;
		}

		// Token: 0x0600059C RID: 1436 RVA: 0x0002530C File Offset: 0x0002370C
		public void conexion_Servidor(IPAddress ip, int puerto)
		{
			try
			{
				this.socket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
				this.buffer = new byte[this.socket.ReceiveBufferSize];
				this.semaforo = new SemaphoreSlim(1);
				this.pings = new List<int>(50);
				this.socket.BeginConnect(ip, puerto, new AsyncCallback(this.conectar_CallBack), this.socket);
			}
			catch (Exception ex)
			{
				Action<string> action = this.socket_informacion;
				if (action != null)
				{
					action(ex.ToString());
				}
				this.get_Desconectar_Socket();
			}
		}

		// Token: 0x0600059D RID: 1437 RVA: 0x000253B4 File Offset: 0x000237B4
		private void conectar_CallBack(IAsyncResult ar)
		{
			try
			{
				bool flag = this.esta_Conectado();
				if (flag)
				{
					this.socket = (ar.AsyncState as Socket);
					this.socket.EndConnect(ar);
					this.socket.BeginReceive(this.buffer, 0, this.buffer.Length, SocketFlags.None, new AsyncCallback(this.recibir_CallBack), this.socket);
					Action<string> action = this.socket_informacion;
					if (action != null)
					{
						action("Bot Socket connecté au serveur dofus");
					}
				}
				else
				{
					this.get_Desconectar_Socket();
					Action<string> action2 = this.socket_informacion;
					if (action2 != null)
					{
						action2("Impossible d'envoyer le socket avec l'hôte");
					}
				}
			}
			catch (Exception ex)
			{
				Action<string> action3 = this.socket_informacion;
				if (action3 != null)
				{
					action3(ex.ToString());
				}
				this.get_Desconectar_Socket();
			}
		}

		// Token: 0x0600059E RID: 1438 RVA: 0x0002548C File Offset: 0x0002388C
		public void recibir_CallBack(IAsyncResult ar)
		{
			bool flag = !this.esta_Conectado() || this.disposed;
			if (flag)
			{
				this.get_Desconectar_Socket();
			}
			else
			{
				SocketError socketError;
				int num = this.socket.EndReceive(ar, out socketError);
				bool flag2 = num > 0 && socketError == SocketError.Success;
				if (flag2)
				{
					string @string = Encoding.UTF8.GetString(this.buffer, 0, num);
					foreach (string text in from x in @string.Replace("\n", string.Empty).Split(new char[1])
					where x != string.Empty
					select x)
					{
						Action<string> action = this.paquete_recibido;
						if (action != null)
						{
							action(text);
						}
						bool flag3 = this.esta_esperando_paquete;
						if (flag3)
						{
							this.pings.Add(Environment.TickCount - this.ticks);
							bool flag4 = this.pings.Count > 48;
							if (flag4)
							{
								this.pings.RemoveAt(1);
							}
							this.esta_esperando_paquete = false;
						}
						PaqueteRecibido.Recibir(this, text);
					}
					bool flag5 = this.esta_Conectado();
					if (flag5)
					{
						this.socket.BeginReceive(this.buffer, 0, this.buffer.Length, SocketFlags.None, new AsyncCallback(this.recibir_CallBack), this.socket);
					}
				}
				else
				{
					this.cuenta.desconectar();
				}
			}
		}

		// Token: 0x0600059F RID: 1439 RVA: 0x00025624 File Offset: 0x00023A24
		public async Task enviar_Paquete_Async(string paquete, bool necesita_respuesta)
		{
			try
			{
				bool flag = !this.esta_Conectado();
				if (!flag)
				{
					paquete += "\n\0";
					byte[] byte_paquete = Encoding.UTF8.GetBytes(paquete);
					await this.semaforo.WaitAsync().ConfigureAwait(false);
					if (necesita_respuesta)
					{
						this.esta_esperando_paquete = true;
					}
					this.socket.Send(byte_paquete);
					if (necesita_respuesta)
					{
						this.ticks = Environment.TickCount;
					}
					Action<string> action = this.paquete_enviado;
					if (action != null)
					{
						action(paquete);
					}
					this.semaforo.Release();
					byte_paquete = null;
				}
			}
			catch (Exception ex)
			{
				Action<string> action2 = this.socket_informacion;
				if (action2 != null)
				{
					action2(ex.ToString());
				}
				this.get_Desconectar_Socket();
			}
		}

		// Token: 0x060005A0 RID: 1440 RVA: 0x00025679 File Offset: 0x00023A79
		public void enviar_Paquete(string paquete, bool necesita_respuesta = false)
		{
			this.enviar_Paquete_Async(paquete, necesita_respuesta).Wait();
		}

		// Token: 0x060005A1 RID: 1441 RVA: 0x0002568C File Offset: 0x00023A8C
		public void get_Desconectar_Socket()
		{
			bool flag = this.esta_Conectado();
			if (flag)
			{
				bool flag2 = this.socket != null && this.socket.Connected;
				if (flag2)
				{
					this.socket.Shutdown(SocketShutdown.Both);
					this.socket.Disconnect(false);
					this.socket.Close();
				}
			}
		}

		// Token: 0x060005A2 RID: 1442 RVA: 0x000256E8 File Offset: 0x00023AE8
		public bool esta_Conectado()
		{
			bool result;
			try
			{
				result = (!this.disposed && this.socket != null && (this.socket.Connected || this.socket.Available != 0));
			}
			catch (SocketException)
			{
				result = false;
			}
			catch (ObjectDisposedException)
			{
				result = false;
			}
			return result;
		}

		// Token: 0x060005A3 RID: 1443 RVA: 0x00025754 File Offset: 0x00023B54
		public int get_Total_Pings()
		{
			return this.pings.Count<int>();
		}

		// Token: 0x060005A4 RID: 1444 RVA: 0x00025761 File Offset: 0x00023B61
		public int get_Promedio_Pings()
		{
			return (int)this.pings.Average();
		}

		// Token: 0x060005A5 RID: 1445 RVA: 0x0002576F File Offset: 0x00023B6F
		public int get_Actual_Ping()
		{
			return Environment.TickCount - this.ticks;
		}

		// Token: 0x060005A6 RID: 1446 RVA: 0x00025780 File Offset: 0x00023B80
		~ClienteTcp()
		{
			this.Dispose(false);
		}

		// Token: 0x060005A7 RID: 1447 RVA: 0x000257B0 File Offset: 0x00023BB0
		public void Dispose()
		{
			this.Dispose(true);
		}

		// Token: 0x060005A8 RID: 1448 RVA: 0x000257BC File Offset: 0x00023BBC
		protected virtual void Dispose(bool disposing)
		{
			bool flag = !this.disposed;
			if (flag)
			{
				bool flag2 = this.socket != null && this.socket.Connected;
				if (flag2)
				{
					this.socket.Shutdown(SocketShutdown.Both);
					this.socket.Disconnect(false);
					this.socket.Close();
				}
				if (disposing)
				{
					this.socket.Dispose();
					this.semaforo.Dispose();
				}
				this.semaforo = null;
				this.cuenta = null;
				this.socket = null;
				this.buffer = null;
				this.paquete_recibido = null;
				this.paquete_enviado = null;
				this.disposed = true;
			}
		}

		// Token: 0x04000389 RID: 905
		public Cuenta cuenta;

		// Token: 0x0400038A RID: 906
		private SemaphoreSlim semaforo;

		// Token: 0x0400038B RID: 907
		private bool disposed;

		// Token: 0x0400038F RID: 911
		private bool esta_esperando_paquete = false;

		// Token: 0x04000390 RID: 912
		private int ticks;

		// Token: 0x04000391 RID: 913
		private List<int> pings;
	}
}
