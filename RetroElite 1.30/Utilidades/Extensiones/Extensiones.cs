using System;
using MoonSharp.Interpreter;
using RetroElite.Otros.Enums;

namespace RetroElite.Utilidades.Extensiones
{
	// Token: 0x02000007 RID: 7
	public static class Extensiones
	{
		// Token: 0x0600001C RID: 28 RVA: 0x00002428 File Offset: 0x00000828
		public static string cadena_Amigable(this EstadoCuenta estado)
		{
			string result;
			switch (estado)
			{
			case EstadoCuenta.CONECTANDO:
				result = "Connecté";
				break;
			case EstadoCuenta.CONECTADO_INACTIVO:
				result = "Inactif";
				break;
			case EstadoCuenta.DESCONECTADO:
				result = "Deconnecté";
				break;
			case EstadoCuenta.MOVIMIENTO:
				result = "Deplacement";
				break;
			case EstadoCuenta.LUCHANDO:
				result = "Combat";
				break;
			case EstadoCuenta.RECOLECTANDO:
				result = "Recolte";
				break;
			case EstadoCuenta.DIALOGANDO:
				result = "Dialogue";
				break;
			case EstadoCuenta.ALMACENAMIENTO:
				result = "Stockage";
				break;
			case EstadoCuenta.INTERCAMBIO:
				result = "Echange";
				break;
			case EstadoCuenta.COMPRANDO:
				result = "Achat";
				break;
			case EstadoCuenta.VENDIENDO:
				result = "Vente";
				break;
			case EstadoCuenta.REGENERANDO:
				result = "Regen vitalité";
				break;
			default:
				result = "-";
				break;
			}
			return result;
		}

		// Token: 0x0600001D RID: 29 RVA: 0x000024DC File Offset: 0x000008DC
		public static T get_Or<T>(this Table table, string key, DataType type, T orValue)
		{
			DynValue dynValue = table.Get(key);
			bool flag = dynValue.IsNil() || dynValue.Type != type;
			T result;
			if (flag)
			{
				result = orValue;
			}
			else
			{
				try
				{
					result = (T)((object)dynValue.ToObject(typeof(T)));
				}
				catch
				{
					result = orValue;
				}
			}
			return result;
		}
	}
}
