using System;
using MoonSharp.Interpreter;

namespace RetroElite.Otros.Scripts.Api
{
	// Token: 0x02000021 RID: 33
	[MoonSharpUserData]
	public class PersonnageApi
	{
		// Token: 0x0600014F RID: 335 RVA: 0x00006886 File Offset: 0x00004C86
		public PersonnageApi(Cuenta _cuenta)
		{
			this.cuenta = _cuenta;
		}

		// Token: 0x06000150 RID: 336 RVA: 0x0000689D File Offset: 0x00004C9D
		public string pseudo()
		{
			return this.cuenta.juego.personaje.nombre;
		}

		// Token: 0x06000151 RID: 337 RVA: 0x000068B4 File Offset: 0x00004CB4
		public byte niveau()
		{
			return this.cuenta.juego.personaje.nivel;
		}

		// Token: 0x06000152 RID: 338 RVA: 0x000068CB File Offset: 0x00004CCB
		public int xp()
		{
			return this.cuenta.juego.personaje.porcentaje_experiencia;
		}

		// Token: 0x06000153 RID: 339 RVA: 0x000068E2 File Offset: 0x00004CE2
		public int kamas()
		{
			return this.cuenta.juego.personaje.kamas;
		}

		// Token: 0x06000154 RID: 340 RVA: 0x000068FC File Offset: 0x00004CFC
		~PersonnageApi()
		{
			this.Dispose(false);
		}

		// Token: 0x06000155 RID: 341 RVA: 0x0000692C File Offset: 0x00004D2C
		public void Dispose()
		{
			this.Dispose(true);
		}

		// Token: 0x06000156 RID: 342 RVA: 0x00006938 File Offset: 0x00004D38
		protected virtual void Dispose(bool disposing)
		{
			bool flag = !this.disposed;
			if (flag)
			{
				this.cuenta = null;
				this.disposed = true;
			}
		}

		// Token: 0x04000075 RID: 117
		private Cuenta cuenta;

		// Token: 0x04000076 RID: 118
		private bool disposed = false;
	}
}
