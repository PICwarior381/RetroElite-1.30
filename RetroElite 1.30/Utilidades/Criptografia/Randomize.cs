using System;
using System.Threading;

namespace RetroElite.Utilidades.Criptografia
{
	// Token: 0x02000009 RID: 9
	internal class Randomize
	{
		// Token: 0x06000026 RID: 38 RVA: 0x000027C8 File Offset: 0x00000BC8
		public static int get_Random(int minimo, int maximo)
		{
			object obj = Randomize.bloqueo;
			int result;
			lock (obj)
			{
				result = ((minimo <= maximo) ? Randomize.random.Value.Next(minimo, maximo) : Randomize.random.Value.Next(maximo, minimo));
			}
			return result;
		}

		// Token: 0x06000027 RID: 39 RVA: 0x00002830 File Offset: 0x00000C30
		public static double get_Random_Numero()
		{
			object obj = Randomize.bloqueo;
			double result;
			lock (obj)
			{
				result = Randomize.random.Value.NextDouble();
			}
			return result;
		}

		// Token: 0x04000010 RID: 16
		private static int seed = Environment.TickCount;

		// Token: 0x04000011 RID: 17
		private static readonly ThreadLocal<Random> random = new ThreadLocal<Random>(() => new Random(Interlocked.Increment(ref Randomize.seed)));

		// Token: 0x04000012 RID: 18
		private static readonly object bloqueo = new object();
	}
}
