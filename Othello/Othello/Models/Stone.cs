using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Othello.Models
{
    class Stone
    {
		public bool IsActive { get; set; }
		private bool color = false;//false = black
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