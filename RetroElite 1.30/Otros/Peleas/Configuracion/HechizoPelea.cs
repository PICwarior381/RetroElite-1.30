using System;
using System.IO;
using RetroElite.Otros.Peleas.Enums;

namespace RetroElite.Otros.Peleas.Configuracion
{
	// Token: 0x0200003D RID: 61
	public class HechizoPelea
	{
		// Token: 0x1700009D RID: 157
		// (get) Token: 0x06000207 RID: 519 RVA: 0x000097C1 File Offset: 0x00007BC1
		// (set) Token: 0x06000208 RID: 520 RVA: 0x000097C9 File Offset: 0x00007BC9
		public short id { get; private set; } = 0;

		// Token: 0x1700009E RID: 158
		// (get) Token: 0x06000209 RID: 521 RVA: 0x000097D2 File Offset: 0x00007BD2
		// (set) Token: 0x0600020A RID: 522 RVA: 0x000097DA File Offset: 0x00007BDA
		public string nombre { get; private set; } = null;

		// Token: 0x1700009F RID: 159
		// (get) Token: 0x0600020B RID: 523 RVA: 0x000097E3 File Offset: 0x00007BE3
		// (set) Token: 0x0600020C RID: 524 RVA: 0x000097EB File Offset: 0x00007BEB
		public HechizoFocus focus { get; private set; }

		// Token: 0x170000A0 RID: 160
		// (get) Token: 0x0600020D RID: 525 RVA: 0x000097F4 File Offset: 0x00007BF4
		// (set) Token: 0x0600020E RID: 526 RVA: 0x000097FC File Offset: 0x00007BFC
		public byte lanzamientos_x_turno { get; set; }

		// Token: 0x170000A1 RID: 161
		// (get) Token: 0x0600020F RID: 527 RVA: 0x00009805 File Offset: 0x00007C05
		// (set) Token: 0x06000210 RID: 528 RVA: 0x0000980D File Offset: 0x00007C0D
		public byte lanzamientos_restantes { get; set; }

		// Token: 0x170000A2 RID: 162
		// (get) Token: 0x06000211 RID: 529 RVA: 0x00009816 File Offset: 0x00007C16
		// (set) Token: 0x06000212 RID: 530 RVA: 0x0000981E File Offset: 0x00007C1E
		public MetodoLanzamiento metodo_lanzamiento { get; private set; }

		// Token: 0x06000213 RID: 531 RVA: 0x00009828 File Offset: 0x00007C28
		public HechizoPelea(short _id, string _nombre, HechizoFocus _focus, MetodoLanzamiento _metodo_lanzamiento, byte _lanzamientos_x_turno)
		{
			this.id = _id;
			this.nombre = _nombre;
			this.focus = _focus;
			this.metodo_lanzamiento = _metodo_lanzamiento;
			this.lanzamientos_restantes = _lanzamientos_x_turno;
			this.lanzamientos_x_turno = _lanzamientos_x_turno;
		}

		// Token: 0x06000214 RID: 532 RVA: 0x00009880 File Offset: 0x00007C80
		public void guardar(BinaryWriter bw)
		{
			bw.Write(this.id);
			bw.Write(this.nombre);
			bw.Write((byte)this.focus);
			bw.Write((byte)this.metodo_lanzamiento);
			bw.Write(this.lanzamientos_x_turno);
		}

		// Token: 0x06000215 RID: 533 RVA: 0x000098D1 File Offset: 0x00007CD1
		public static HechizoPelea cargar(BinaryReader br)
		{
			return new HechizoPelea(br.ReadInt16(), br.ReadString(), (HechizoFocus)br.ReadByte(), (MetodoLanzamiento)br.ReadByte(), br.ReadByte());
		}
	}
}
