using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading;
using RetroElite.Otros.Scripts.Acciones;

namespace RetroElite.Otros.Grupos
{
	// Token: 0x02000040 RID: 64
	public class Grupo : IDisposable
	{
		// Token: 0x170000AB RID: 171
		// (get) Token: 0x06000230 RID: 560 RVA: 0x00009CB8 File Offset: 0x000080B8
		// (set) Token: 0x06000231 RID: 561 RVA: 0x00009CC0 File Offset: 0x000080C0
		public Cuenta lider { get; private set; }

		// Token: 0x170000AC RID: 172
		// (get) Token: 0x06000232 RID: 562 RVA: 0x00009CC9 File Offset: 0x000080C9
		// (set) Token: 0x06000233 RID: 563 RVA: 0x00009CD1 File Offset: 0x000080D1
		public ObservableCollection<Cuenta> miembros { get; private set; }

		// Token: 0x06000234 RID: 564 RVA: 0x00009CDC File Offset: 0x000080DC
		public Grupo(Cuenta _lider)
		{
			this.agrupamiento = new Agrupamiento(this);
			this.cuentas_acabadas = new Dictionary<Cuenta, ManualResetEvent>();
			this.lider = _lider;
			this.miembros = new ObservableCollection<Cuenta>();
			this.lider.grupo = this;
		}

		// Token: 0x06000235 RID: 565 RVA: 0x00009D2C File Offset: 0x0000812C
		public void agregar_Miembro(Cuenta miembro)
		{
			bool flag = this.miembros.Count >= 7;
			if (!flag)
			{
				miembro.grupo = this;
				this.miembros.Add(miembro);
				this.cuentas_acabadas.Add(miembro, new ManualResetEvent(false));
			}
		}

		// Token: 0x06000236 RID: 566 RVA: 0x00009D79 File Offset: 0x00008179
		public void eliminar_Miembro(Cuenta miembro)
		{
			this.miembros.Remove(miembro);
		}

		// Token: 0x06000237 RID: 567 RVA: 0x00009D88 File Offset: 0x00008188
		public void conectar_Cuentas()
		{
			this.lider.conectar();
			foreach (Cuenta cuenta in this.miembros)
			{
				cuenta.conectar();
			}
		}

		// Token: 0x06000238 RID: 568 RVA: 0x00009DE4 File Offset: 0x000081E4
		public void desconectar_Cuentas()
		{
			foreach (Cuenta cuenta in this.miembros)
			{
				cuenta.desconectar();
			}
		}

		// Token: 0x06000239 RID: 569 RVA: 0x00009E34 File Offset: 0x00008234
		public void enqueue_Acciones_Miembros(AccionesScript accion, bool iniciar_dequeue = false)
		{
			bool flag = accion is PeleasAccion;
			if (flag)
			{
				foreach (Cuenta key in this.miembros)
				{
					this.cuentas_acabadas[key].Set();
				}
			}
			else
			{
				foreach (Cuenta cuenta in this.miembros)
				{
					cuenta.script.manejar_acciones.enqueue_Accion(accion, iniciar_dequeue);
				}
				if (iniciar_dequeue)
				{
					for (int i = 0; i < this.miembros.Count; i++)
					{
						this.cuentas_acabadas[this.miembros[i]].Reset();
					}
				}
			}
		}

		// Token: 0x0600023A RID: 570 RVA: 0x00009F38 File Offset: 0x00008338
		public void esperar_Acciones_Terminadas()
		{
			WaitHandle[] waitHandles = this.cuentas_acabadas.Values.ToArray<ManualResetEvent>();
			WaitHandle.WaitAll(waitHandles);
		}

		// Token: 0x0600023B RID: 571 RVA: 0x00009F5D File Offset: 0x0000835D
		private void miembro_Acciones_Acabadas(Cuenta cuenta)
		{
			cuenta.logger.log_informacion("GRUPO", "Acciones acabadas");
			this.cuentas_acabadas[cuenta].Set();
		}

		// Token: 0x0600023C RID: 572 RVA: 0x00009F88 File Offset: 0x00008388
		public void Dispose()
		{
			this.Dispose(true);
		}

		// Token: 0x0600023D RID: 573 RVA: 0x00009F94 File Offset: 0x00008394
		~Grupo()
		{
			this.Dispose(false);
		}

		// Token: 0x0600023E RID: 574 RVA: 0x00009FC4 File Offset: 0x000083C4
		protected virtual void Dispose(bool disposing)
		{
			bool flag = !this.disposed;
			if (flag)
			{
				if (disposing)
				{
					this.agrupamiento.Dispose();
					this.lider.Dispose();
					for (int i = 0; i < this.miembros.Count; i++)
					{
						this.miembros[i].Dispose();
					}
				}
				this.agrupamiento = null;
				this.cuentas_acabadas.Clear();
				this.cuentas_acabadas = null;
				this.miembros.Clear();
				this.miembros = null;
				this.lider = null;
				this.disposed = true;
			}
		}

		// Token: 0x040000E6 RID: 230
		private Agrupamiento agrupamiento;

		// Token: 0x040000E7 RID: 231
		private Dictionary<Cuenta, ManualResetEvent> cuentas_acabadas;

		// Token: 0x040000EA RID: 234
		private bool disposed;
	}
}
