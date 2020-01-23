using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using RetroElite.Otros.Game.Personaje.Inventario;

namespace RetroElite.Otros.Scripts.Acciones.Almacenamiento
{
	// Token: 0x02000030 RID: 48
	internal class AlmacenarTodosLosObjetosAccion : AccionesScript
	{
		// Token: 0x06000199 RID: 409 RVA: 0x00007424 File Offset: 0x00005824
		internal override async Task<ResultadosAcciones> proceso(Cuenta cuenta)
		{
			InventarioGeneral inventario = cuenta.juego.personaje.inventario;
			foreach (ObjetosInventario objeto in inventario.objetos)
			{
				bool flag = !objeto.objeto_esta_equipado();
				if (flag)
				{
					cuenta.conexion.enviar_Paquete(string.Format("EMO+{0}|{1}", objeto.id_inventario, objeto.cantidad), false);
					inventario.eliminar_Objeto(objeto, 0, false);
					await Task.Delay(300);
				}
				objeto = null;
			}
			IEnumerator<ObjetosInventario> enumerator = null;
			return ResultadosAcciones.HECHO;
		}
	}
}
