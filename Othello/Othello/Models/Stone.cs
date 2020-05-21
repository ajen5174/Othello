using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Othello.Models
{
    class Stone
    {
		private bool color;
		public bool Color
		{
			get { return color; }
		}

		public void Flip()
		{
			color = !color;

			// other logic here
		}
	}
}