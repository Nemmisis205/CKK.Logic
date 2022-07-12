using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CKK.Logic.Exceptions;

namespace CKK.Logic.Interfaces
{
	public abstract class Entity
	{
		private int _id;
		public int Id 
		{ 
			get => _id;
            set
            {
                //           try
                //           {
                //if (value < 0) { throw new InvalidIdException(); }
                //else { _id = value; }
                //           }
                //           catch (InvalidIdException) { Console.WriteLine("ID must not be negative."); }
                if (value < 0) { throw new InvalidIdException(); }
                else { _id = value; }
            }
		}
		public string Name { get; set; }
	}
}
