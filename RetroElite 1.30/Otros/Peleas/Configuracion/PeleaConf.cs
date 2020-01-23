using System;
using System.Collections.Generic;
using System.IO;
using RetroElite.Otros.Peleas.Enums;

namespace RetroElite.Otros.Peleas.Configuracion
{
	// Token: 0x0200003E RID: 62
	public class PeleaConf : IDisposable
	{
		// Token: 0x170000A3 RID: 163
		// (get) Token: 0x06000216 RID: 534 RVA: 0x000098F6 File Offset: 0x00007CF6
		private string archivo_configuracion
		{
			get
			{
				return Path.Combine("fight/", this.cuenta.juego.personaje.nombre + ".config");
			}
		}

		// Token: 0x170000A4 RID: 164
		// (get) Token: 0x06000217 RID: 535 RVA: 0x00009921 File Offset: 0x00007D21
		// (set) Token: 0x06000218 RID: 536 RVA: 0x00009929 File Offset: 0x00007D29
		public List<HechizoPelea> hechizos { get; private set; }

		// Token: 0x170000A5 RID: 165
		// (get) Token: 0x06000219 RID: 537 RVA: 0x00009932 File Offset: 0x00007D32
		// (set) Token: 0x0600021A RID: 538 RVA: 0x0000993A File Offset: 0x00007D3A
		public bool desactivar_espectador { get; set; }

		// Token: 0x170000A6 RID: 166
		// (get) Token: 0x0600021B RID: 539 RVA: 0x00009943 File Offset: 0x00007D43
		// (set) Token: 0x0600021C RID: 540 RVA: 0x0000994B File Offset: 0x00007D4B
		public bool utilizar_dragopavo { get; set; }

		// Token: 0x170000A7 RID: 167
		// (get) Token: 0x0600021D RID: 541 RVA: 0x00009954 File Offset: 0x00007D54
		// (set) Token: 0x0600021E RID: 542 RVA: 0x0000995C File Offset: 0x00007D5C
		public Tactica tactica { get; set; }

		// Token: 0x170000A8 RID: 168
		// (get) Token: 0x0600021F RID: 543 RVA: 0x00009965 File Offset: 0x00007D65
		// (set) Token: 0x06000220 RID: 544 RVA: 0x0000996D File Offset: 0x00007D6D
		public byte iniciar_regeneracion { get; set; }

		// Token: 0x170000A9 RID: 169
		// (get) Token: 0x06000221 RID: 545 RVA: 0x00009976 File Offset: 0x00007D76
		// (set) Token: 0x06000222 RID: 546 RVA: 0x0000997E File Offset: 0x00007D7E
		public byte detener_regeneracion { get; set; }

		// Token: 0x170000AA RID: 170
		// (get) Token: 0x06000223 RID: 547 RVA: 0x00009987 File Offset: 0x00007D87
		// (set) Token: 0x06000224 RID: 548 RVA: 0x0000998F File Offset: 0x00007D8F
		public PosicionamientoInicioPelea posicionamiento { get; set; }

		// Token: 0x06000225 RID: 549 RVA: 0x00009998 File Offset: 0x00007D98
		public PeleaConf(Cuenta _cuenta)
		{
			this.cuenta = _cuenta;
			this.hechizos = new List<HechizoPelea>();
		}

		// Token: 0x06000226 RID: 550 RVA: 0x000099B8 File Offset: 0x00007DB8
		public void guardar()
		{
			Directory.CreateDirectory("fight/");
			using (BinaryWriter binaryWriter = new BinaryWriter(File.Open(this.archivo_configuracion, FileMode.Create)))
			{
				binaryWriter.Write((byte)this.tactica);
				binaryWriter.Write((byte)this.posicionamiento);
				binaryWriter.Write(this.desactivar_espectador);
				binaryWriter.Write(this.utilizar_dragopavo);
				binaryWriter.Write(this.iniciar_regeneracion);
				binaryWriter.Write(this.detener_regeneracion);
				binaryWriter.Write((byte)this.hechizos.Count);
				for (int i = 0; i < this.hechizos.Count; i++)
				{
					this.hechizos[i].guardar(binaryWriter);
				}
			}
		}

		// Token: 0x06000227 RID: 551 RVA: 0x00009A94 File Offset: 0x00007E94
		public void cargar()
		{
			bool flag = !File.Exists(this.archivo_configuracion);
			if (flag)
			{
				this.get_Perfil_Defecto();
			}
			else
			{
				using (BinaryReader binaryReader = new BinaryReader(File.Open(this.archivo_configuracion, FileMode.Open)))
				{
					this.tactica = (Tactica)binaryReader.ReadByte();
					this.posicionamiento = (PosicionamientoInicioPelea)binaryReader.ReadByte();
					this.desactivar_espectador = binaryReader.ReadBoolean();
					this.utilizar_dragopavo = binaryReader.ReadBoolean();
					this.iniciar_regeneracion = binaryReader.ReadByte();
					this.detener_regeneracion = binaryReader.ReadByte();
					this.hechizos.Clear();
					byte b = binaryReader.ReadByte();
					for (int i = 0; i < (int)b; i++)
					{
						this.hechizos.Add(HechizoPelea.cargar(binaryReader));
					}
				}
			}
		}

		// Token: 0x06000228 RID: 552 RVA: 0x00009B78 File Offset: 0x00007F78
		private void get_Perfil_Defecto()
		{
			this.desactivar_espectador = true;
			this.utilizar_dragopavo = false;
			this.tactica = Tactica.AGRESIVA;
			this.posicionamiento = PosicionamientoInicioPelea.CERCA_DE_ENEMIGOS;
			this.iniciar_regeneracion = 70;
			this.detener_regeneracion = 100;
		}

		// Token: 0x06000229 RID: 553 RVA: 0x00009BAD File Offset: 0x00007FAD
		public void Dispose()
		{
			this.Dispose(true);
		}

		// Token: 0x0600022A RID: 554 RVA: 0x00009BB8 File Offset: 0x00007FB8
		~PeleaConf()
		{
			this.Dispose(false);
		}

		// Token: 0x0600022B RID: 555 RVA: 0x00009BE8 File Offset: 0x00007FE8
		protected virtual void Dispose(bool disposing)
		{
			bool flag = !this.disposed;
			if (flag)
			{
				this.hechizos.Clear();
				this.hechizos = null;
				this.cuenta = null;
				this.disposed = true;
			}
		}

		// Token: 0x040000D9 RID: 217
		private const string carpeta_configuracion = "fight/";

		// Token: 0x040000DA RID: 218
		private Cuenta cuenta;

		// Token: 0x040000DB RID: 219
		private bool disposed;
	}
}
