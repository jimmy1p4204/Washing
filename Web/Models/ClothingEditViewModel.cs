using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Web.Models
{
	public class ClothingEditViewModel : Clothing
	{
		// this may have to be a List<SelectListItems> to work with MultiSelectList - check.
		public MultiSelectList Colors { get; set; }
		public List<int> SelectedColorIds { get; set; } = new List<int>();
	}
}
