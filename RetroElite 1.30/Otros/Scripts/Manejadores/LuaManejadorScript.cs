using System;
using System.Collections.Generic;
using System.Linq;
using MoonSharp.Interpreter;

namespace RetroElite.Otros.Scripts.Manejadores
{
	// Token: 0x02000014 RID: 20
	public class LuaManejadorScript : IDisposable
	{
		// Token: 0x1700006B RID: 107
		// (get) Token: 0x060000EC RID: 236 RVA: 0x0000540B File Offset: 0x0000380B
		// (set) Token: 0x060000ED RID: 237 RVA: 0x00005413 File Offset: 0x00003813
		public Script script { get; private set; }

		// Token: 0x060000EE RID: 238 RVA: 0x0000541C File Offset: 0x0000381C
		public void cargar_Desde_Archivo(string ruta_archivo, Action funciones_Personalizadas)
		{
			this.script = new Script();
			funciones_Personalizadas();
			this.script.DoFile(ruta_archivo, null, null);
		}

		// Token: 0x060000EF RID: 239 RVA: 0x00005444 File Offset: 0x00003844
		public IEnumerable<Table> get_Entradas_Funciones(string nombre_funcion)
		{
			DynValue dynValue = this.script.Globals.Get(nombre_funcion);
			bool flag = dynValue.IsNil() || dynValue.Type != DataType.Function;
			IEnumerable<Table> result;
			if (flag)
			{
				result = null;
			}
			else
			{
				DynValue dynValue2 = this.script.Call(dynValue);
				IEnumerable<Table> enumerable;
				if (dynValue2.Type == DataType.Table)
				{
					enumerable = from f in dynValue2.Table.Values
					where f.Type == DataType.Table
					select f.Table;
				}
				else
				{
					enumerable = null;
				}
				result = enumerable;
			}
			return result;
		}

		// Token: 0x060000F0 RID: 240 RVA: 0x000054F4 File Offset: 0x000038F4
		public T get_Global_Or<T>(string key, DataType tipo, T valor_or)
		{
			DynValue dynValue = this.script.Globals.Get(key);
			bool flag = dynValue.IsNil() || dynValue.Type != tipo;
			T result;
			if (flag)
			{
				result = valor_or;
			}
			else
			{
				try
				{
					result = (T)((object)dynValue.ToObject(typeof(T)));
				}
				catch
				{
					result = valor_or;
				}
			}
			return result;
		}

		// Token: 0x060000F1 RID: 241 RVA: 0x00005564 File Offset: 0x00003964
		public DynValue get_Global_como_Dyn_Valor(string key)
		{
			return this.script.Globals.Get(key);
		}

		// Token: 0x060000F2 RID: 242 RVA: 0x00005577 File Offset: 0x00003977
		public T get_Global_Or<T>(string key, T or)
		{
			return this.es_Global(key) ? ((T)((object)this.script.Globals[key])) : or;
		}

		// Token: 0x060000F3 RID: 243 RVA: 0x0000559C File Offset: 0x0000399C
		public T get_Global<T>(string key)
		{
			return this.es_Global(key) ? ((T)((object)this.script.Globals[key])) : default(T);
		}

		// Token: 0x060000F4 RID: 244 RVA: 0x000055D3 File Offset: 0x000039D3
		public bool es_Global(string key)
		{
			return this.script.Globals[key] != null;
		}

		// Token: 0x060000F5 RID: 245 RVA: 0x000055E9 File Offset: 0x000039E9
		public void Set_Global(string key, object value)
		{
			this.script.Globals[key] = value;
		}

		// Token: 0x060000F6 RID: 246 RVA: 0x000055FE File Offset: 0x000039FE
		public static void inicializar_Funciones()
		{
			UserData.RegisterAssembly(null, false);
		}

		// Token: 0x060000F7 RID: 247 RVA: 0x00005608 File Offset: 0x00003A08
		~LuaManejadorScript()
		{
			this.Dispose(false);
		}

		// Token: 0x060000F8 RID: 248 RVA: 0x00005638 File Offset: 0x00003A38
		public void Dispose()
		{
			this.Dispose(true);
		}

		// Token: 0x060000F9 RID: 249 RVA: 0x00005644 File Offset: 0x00003A44
		protected virtual void Dispose(bool disposing)
		{
			bool flag = this.disposed;
			if (flag)
			{
				this.script = null;
				this.disposed = true;
			}
		}

		// Token: 0x04000053 RID: 83
		private bool disposed = false;
	}
}
