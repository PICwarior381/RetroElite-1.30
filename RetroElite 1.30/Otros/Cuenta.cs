using System;
using System.Diagnostics;
using System.Net;
using RetroElite.Comun.Network;
using RetroElite.Otros.Enums;
using RetroElite.Otros.Game;
using RetroElite.Otros.Grupos;
using RetroElite.Otros.Peleas;
using RetroElite.Otros.Scripts;
using RetroElite.Utilidades.Configuracion;
using RetroElite.Utilidades.Logs;

namespace RetroElite.Otros
{
	// Token: 0x0200000E RID: 14
	public class Cuenta : IDisposable
	{
		// Token: 0x1700005A RID: 90
		// (get) Token: 0x06000093 RID: 147 RVA: 0x00003C86 File Offset: 0x00002086
		// (set) Token: 0x06000094 RID: 148 RVA: 0x00003C8E File Offset: 0x0000208E
		public string apodo { get; set; } = string.Empty;

		// Token: 0x1700005B RID: 91
		// (get) Token: 0x06000095 RID: 149 RVA: 0x00003C97 File Offset: 0x00002097
		// (set) Token: 0x06000096 RID: 150 RVA: 0x00003C9F File Offset: 0x0000209F
		public string key_bienvenida { get; set; } = string.Empty;

		// Token: 0x1700005C RID: 92
		// (get) Token: 0x06000097 RID: 151 RVA: 0x00003CA8 File Offset: 0x000020A8
		// (set) Token: 0x06000098 RID: 152 RVA: 0x00003CB0 File Offset: 0x000020B0
		public string tiquet_game { get; set; } = string.Empty;

		// Token: 0x1700005D RID: 93
		// (get) Token: 0x06000099 RID: 153 RVA: 0x00003CB9 File Offset: 0x000020B9
		// (set) Token: 0x0600009A RID: 154 RVA: 0x00003CC1 File Offset: 0x000020C1
		public Logger logger { get; private set; }

		// Token: 0x1700005E RID: 94
		// (get) Token: 0x0600009B RID: 155 RVA: 0x00003CCA File Offset: 0x000020CA
		// (set) Token: 0x0600009C RID: 156 RVA: 0x00003CD2 File Offset: 0x000020D2
		public ClienteTcp conexion { get; set; }

		// Token: 0x1700005F RID: 95
		// (get) Token: 0x0600009D RID: 157 RVA: 0x00003CDB File Offset: 0x000020DB
		// (set) Token: 0x0600009E RID: 158 RVA: 0x00003CE3 File Offset: 0x000020E3
		public Juego juego { get; private set; }

		// Token: 0x17000060 RID: 96
		// (get) Token: 0x0600009F RID: 159 RVA: 0x00003CEC File Offset: 0x000020EC
		// (set) Token: 0x060000A0 RID: 160 RVA: 0x00003CF4 File Offset: 0x000020F4
		public ManejadorScript script { get; set; }

		// Token: 0x17000061 RID: 97
		// (get) Token: 0x060000A1 RID: 161 RVA: 0x00003CFD File Offset: 0x000020FD
		// (set) Token: 0x060000A2 RID: 162 RVA: 0x00003D05 File Offset: 0x00002105
		public PeleaExtensiones pelea_extension { get; set; }

		// Token: 0x17000062 RID: 98
		// (get) Token: 0x060000A3 RID: 163 RVA: 0x00003D0E File Offset: 0x0000210E
		// (set) Token: 0x060000A4 RID: 164 RVA: 0x00003D16 File Offset: 0x00002116
		public CuentaConf configuracion { get; private set; }

		// Token: 0x17000063 RID: 99
		// (get) Token: 0x060000A5 RID: 165 RVA: 0x00003D1F File Offset: 0x0000211F
		// (set) Token: 0x060000A6 RID: 166 RVA: 0x00003D27 File Offset: 0x00002127
		public Grupo grupo { get; set; }

		// Token: 0x17000064 RID: 100
		// (get) Token: 0x060000A7 RID: 167 RVA: 0x00003D30 File Offset: 0x00002130
		public bool tiene_grupo
		{
			get
			{
				return this.grupo != null;
			}
		}

		// Token: 0x17000065 RID: 101
		// (get) Token: 0x060000A8 RID: 168 RVA: 0x00003D3B File Offset: 0x0000213B
		public bool es_lider_grupo
		{
			get
			{
				return !this.tiene_grupo || this.grupo.lider == this;
			}
		}

		// Token: 0x14000002 RID: 2
		// (add) Token: 0x060000A9 RID: 169 RVA: 0x00003D58 File Offset: 0x00002158
		// (remove) Token: 0x060000AA RID: 170 RVA: 0x00003D90 File Offset: 0x00002190
		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		public event Action evento_estado_cuenta;

		// Token: 0x14000003 RID: 3
		// (add) Token: 0x060000AB RID: 171 RVA: 0x00003DC8 File Offset: 0x000021C8
		// (remove) Token: 0x060000AC RID: 172 RVA: 0x00003E00 File Offset: 0x00002200
		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		public event Action cuenta_desconectada;

		// Token: 0x060000AD RID: 173 RVA: 0x00003E38 File Offset: 0x00002238
		public Cuenta(CuentaConf _configuracion)
		{
			this.configuracion = _configuracion;
			this.logger = new Logger();
			this.juego = new Juego(this);
			this.pelea_extension = new PeleaExtensiones(this);
			this.script = new ManejadorScript(this);
		}

		// Token: 0x060000AE RID: 174 RVA: 0x00003EBE File Offset: 0x000022BE
		public void conectar()
		{
			this.conexion = new ClienteTcp(this);
			this.conexion.conexion_Servidor(IPAddress.Parse(GlobalConf.ip_conexion), (int)GlobalConf.puerto_conexion);
		}

		// Token: 0x060000AF RID: 175 RVA: 0x00003EEC File Offset: 0x000022EC
		public void desconectar()
		{
			ClienteTcp conexion = this.conexion;
			if (conexion != null)
			{
				conexion.Dispose();
			}
			this.conexion = null;
			this.script.detener_Script("script");
			this.juego.limpiar();
			this.Estado_Cuenta = EstadoCuenta.DESCONECTADO;
			Action action = this.cuenta_desconectada;
			if (action != null)
			{
				action();
			}
		}

		// Token: 0x060000B0 RID: 176 RVA: 0x00003F4B File Offset: 0x0000234B
		public void cambiando_Al_Servidor_Juego(string ip, int puerto)
		{
			this.conexion.get_Desconectar_Socket();
			this.conexion.conexion_Servidor(IPAddress.Parse(ip), puerto);
		}

		// Token: 0x17000066 RID: 102
		// (get) Token: 0x060000B1 RID: 177 RVA: 0x00003F6D File Offset: 0x0000236D
		// (set) Token: 0x060000B2 RID: 178 RVA: 0x00003F75 File Offset: 0x00002375
		public EstadoCuenta Estado_Cuenta
		{
			get
			{
				return this.estado_cuenta;
			}
			set
			{
				this.estado_cuenta = value;
				Action action = this.evento_estado_cuenta;
				if (action != null)
				{
					action();
				}
			}
		}

		// Token: 0x060000B3 RID: 179 RVA: 0x00003F91 File Offset: 0x00002391
		public bool esta_ocupado()
		{
			return this.Estado_Cuenta != EstadoCuenta.CONECTADO_INACTIVO && this.Estado_Cuenta != EstadoCuenta.REGENERANDO;
		}

		// Token: 0x060000B4 RID: 180 RVA: 0x00003FAC File Offset: 0x000023AC
		public bool esta_dialogando()
		{
			return this.Estado_Cuenta == EstadoCuenta.ALMACENAMIENTO || this.Estado_Cuenta == EstadoCuenta.DIALOGANDO || this.Estado_Cuenta == EstadoCuenta.INTERCAMBIO || this.Estado_Cuenta == EstadoCuenta.COMPRANDO || this.Estado_Cuenta == EstadoCuenta.VENDIENDO;
		}

		// Token: 0x060000B5 RID: 181 RVA: 0x00003FE0 File Offset: 0x000023E0
		public bool esta_luchando()
		{
			return this.Estado_Cuenta == EstadoCuenta.LUCHANDO;
		}

		// Token: 0x060000B6 RID: 182 RVA: 0x00003FEC File Offset: 0x000023EC
		public bool esta_recolectando()
		{
			return this.Estado_Cuenta == EstadoCuenta.RECOLECTANDO;
		}

		// Token: 0x060000B7 RID: 183 RVA: 0x00004007 File Offset: 0x00002407
		public bool esta_desplazando()
		{
			return this.Estado_Cuenta == EstadoCuenta.MOVIMIENTO;
		}

		// Token: 0x060000B8 RID: 184 RVA: 0x00004012 File Offset: 0x00002412
		public void Dispose()
		{
			this.Dispose(true);
		}

		// Token: 0x060000B9 RID: 185 RVA: 0x0000401C File Offset: 0x0000241C
		~Cuenta()
		{
			this.Dispose(false);
		}

		// Token: 0x060000BA RID: 186 RVA: 0x0000404C File Offset: 0x0000244C
		public virtual void Dispose(bool disposing)
		{
			bool flag = !this.disposed;
			if (flag)
			{
				if (disposing)
				{
					this.script.Dispose();
					ClienteTcp conexion = this.conexion;
					if (conexion != null)
					{
						conexion.Dispose();
					}
					this.juego.Dispose();
				}
				this.Estado_Cuenta = EstadoCuenta.DESCONECTADO;
				this.script = null;
				this.key_bienvenida = null;
				this.conexion = null;
				this.logger = null;
				this.juego = null;
				this.apodo = null;
				this.configuracion = null;
				this.disposed = true;
			}
		}

		// Token: 0x04000028 RID: 40
		private EstadoCuenta estado_cuenta = EstadoCuenta.DESCONECTADO;

		// Token: 0x04000029 RID: 41
		public bool puede_utilizar_dragopavo = false;

		// Token: 0x0400002B RID: 43
		private bool disposed;

		// Token: 0x0400002E RID: 46
		private int contador_recoleccion = 0;
	}
}
