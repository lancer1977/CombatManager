/*
 *  GenericHtmlCreator.cs
 *
 *  Copyright (C) 2010-2012 Kyle Olson, kyle@kyleolson.com
 *
 *  This program is free software; you can redistribute it and/or
 *  modify it under the terms of the GNU General Public License
 *  as published by the Free Software Foundation; either version 2
 *  of the License, or (at your option) any later version.
 *
 *  This program is distributed in the hope that it will be useful,
 *  but WITHOUT ANY WARRANTY; without even the implied warranty of
 *  MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
 *  GNU General Public License for more details.
 * 
 *  You should have received a copy of the GNU General Public License
 *  along with this program; if not, write to the Free Software
 *  Foundation, Inc., 51 Franklin Street, Fifth Floor, Boston, MA  02110-1301, USA.
 *
 */

using System;
using System.Text;
using CombatManager;
using System.Collections.Generic;


namespace CombatManager.Html
{
	public class GenericHtmlCreator
	{
		public GenericHtmlCreator ()
		{
		}

        public static string CreateHtml(String title, string text)
        {
        	StringBuilder blocks = new StringBuilder();
			blocks.CreateHtmlHeader();
			
            blocks.CreateHeader(title);
                
			blocks.AppendHtml(text);
			
			blocks.CreateHtmlFooter();


            return blocks.ToString();

        }
	}
}

