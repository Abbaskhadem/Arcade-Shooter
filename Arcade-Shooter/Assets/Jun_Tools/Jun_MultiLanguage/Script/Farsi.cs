﻿//##############################################################################
//#                                                                            #
//#   Unity Farsi                                                              #
//#                                                                            #
//#   @Copyright (C) 2014 By Mehran M. Spitmaan                                #
//#         from Azdaa (mehran.m.spitman@gmail.com                             #
//#                                                                            #
//# 	                                                                       #
//#   This program is free software: you can redistribute it and/or modify     #
//#   it under the terms of the GNU General Public License as published by     #
//#   the Free Software Foundation, either version 3 of the License, or        #
//#   (at your option) any later version.                                      #
//# 	                                                                       #
//#   This program is distributed in the hope that it will be useful,          #
//#   but WITHOUT ANY WARRANTY; without even the implied warranty of           #
//#   MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the            #
//#   GNU General Public License for more details.                             #
//#                                                                            #
//##############################################################################

using System.Collections.Generic;
using System.IO;

namespace System.Collections
{
	public static class Farsi
	{

		//Key: converted letter to - Value: Basic type letter
		public static Dictionary<int, int> basicCharStorage = new Dictionary<int, int>()
		{
			{32, 32},
			{65165, 65165}, {65166, 65165}, {65167, 65167}, {65168, 65167}, {65169, 65167}, 
			{65170, 65167}, {64342, 64342}, {64343, 64342}, {64344, 64342}, {64345, 64342}, {65173, 65173}, {65174, 65173}, 
			{65175, 65173}, {65176, 65173}, {65177, 65177}, {65178, 65177}, {65179, 65177}, {65180, 65177}, {65181, 65181}, 
			{65182, 65181}, {65183, 65181}, {65184, 65181}, {64378, 64378}, {64379, 64378}, {64380, 64378}, {64381, 64378}, 
			{65185, 65185}, {65186, 65185}, {65187, 65185}, {65188, 65185}, {65189, 65189}, {65190, 65189}, {65191, 65189}, 
			{65192, 65189}, {65193, 65193}, {65194, 65193}, {65195, 65195}, {65196, 65195}, {65197, 65197}, {65198, 65197}, 
			{65199, 65199}, {65200, 65199}, {64394, 64394}, {64395, 64394}, 
			{65201, 65201}, {65202, 65201}, {65203, 65201}, {65204, 65201}, {65205, 65205}, {65206, 65205}, {65207, 65205}, 
			{65208, 65205}, {65209, 65209}, {65210, 65209}, {65211, 65209}, {65212, 65209}, {65213, 65213}, {65214, 65213}, 
			{65215, 65213}, {65216, 65213}, {65217, 65217}, {65218, 65217}, {65219, 65217}, {65220, 65217}, {65221, 65221}, 
			{65222, 65221}, {65223, 65221}, {65224, 65221}, {65225, 65225}, {65226, 65225}, {65227, 65225}, {65228, 65225}, 
			{65229, 65229}, {65230, 65229}, {65231, 65229}, {65232, 65229}, {65233, 65233}, {65234, 65233}, {65235, 65233}, 
			{65236, 65233}, {65237, 65237}, {65238, 65237}, {65239, 65237}, {65240, 65237}, {64398, 64398}, {64399, 64398}, 
			{64400, 64398}, {64401, 64398}, {64402, 64402}, {64403, 64402}, {64404, 64402}, {64405, 64402}, {65245, 65245}, 
			{65246, 65245}, {65247, 65245}, {65248, 65245}, {65249, 65249}, {65250, 65249}, {65251, 65249}, {65252, 65249}, 
			{65253, 65253}, {65254, 65253}, {65255, 65253}, {65256, 65253}, {65261, 65261}, {65262, 65261}, 
			{65257, 65257}, {65258, 65257}, {65259, 65257}, {65260, 65257}, {64508, 64508}, {64509, 64508}, 
			{64510, 64508}, {64511, 64508}, {65153, 65153}, {65154, 65153}, {1575, 1575}, {1576, 1576}, {1662, 1662}, 
			{1578, 1578}, {1579, 1579}, {1580, 1580}, {1670, 1670}, {1581, 1581}, {1582, 1582}, {1583, 1583}, 
			{1584, 1584}, {1585, 1585}, {1586, 1586}, {1688, 1688}, {1587, 1587}, {1588, 1588}, {1589, 1589}, 
			{1590, 1590}, {1591, 1591}, {1592, 1592}, {1593, 1593}, {1594, 1594}, {1601, 1601}, {1602, 1602}, 
			{1705, 1705}, {1711, 1711}, {1604, 1604}, {1605, 1605}, {1606, 1606}, {1608, 1608}, {1607, 1607}, 
			{1740, 1740}, {1570, 1570}, {65265, 65265}, {65266, 65265}, {65267, 65265}, {65268, 65265}, {65171, 65171},
			{65172, 65171}, {65263, 65263}, {65264, 65263}, {65157, 65157}, {65158, 65157}, {65155, 65155}, 
			{65156, 65155}, {65159, 65159}, {65160, 65159}, {65241, 65241}, {65242, 65241}, 
			{65243, 65241}, {65244, 65241}, {65161, 65161}, {65162, 65161}, {65163, 65161}, {65164, 65161}
		};
		
		//Basic type letter
		public static Dictionary <int,int[]> charStorage = new Dictionary<int, int[]> ()
		{
			{65165, new int[] {65165, 65166, 65165, 65166}},	{65167, new int[] {65167, 65168, 65169, 65170}},	{64342, new int[] {64342, 64343, 64344, 64345}},	{65173, new int[] {65173, 65174, 65175, 65176}},	{65177, new int[] {65177, 65178, 65179, 65180}},	{65181, new int[] {65181, 65182, 65183, 65184}},	{64378, new int[] {64378, 64379, 64380, 64381}},	{65185, new int[] {65185, 65186, 65187, 65188}},	{65189, new int[] {65189, 65190, 65191, 65192}},	{65193, new int[] {65193, 65194, 65193, 65194}},	{65195, new int[] {65195, 65196, 65195, 65196}},	{65197, new int[] {65197, 65198, 65197, 65198}},	{65199, new int[] {65199, 65200, 65199, 65200}},	{64394, new int[] {64394, 64395, 64394, 64395}},	{65201, new int[] {65201, 65202, 65203, 65204}},	{65205, new int[] {65205, 65206, 65207, 65208}},	{65209, new int[] {65209, 65210, 65211, 65212}},	{65213, new int[] {65213, 65214, 65215, 65216}},	{65217, new int[] {65217, 65218, 65219, 65220}},	{65221, new int[] {65221, 65222, 65223, 65224}},	{65225, new int[] {65225, 65226, 65227, 65228}},	{65229, new int[] {65229, 65230, 65231, 65232}},	{65233, new int[] {65233, 65234, 65235, 65236}},	{65237, new int[] {65237, 65238, 65239, 65240}},	{64398, new int[] {64398, 64399, 64400, 64401}},	{64402, new int[] {64402, 64403, 64404, 64405}},	{65245, new int[] {65245, 65246, 65247, 65248}},	{65249, new int[] {65249, 65250, 65251, 65252}},	{65253, new int[] {65253, 65254, 65255, 65256}},	{65261, new int[] {65261, 65262, 65261, 65262}},	{65257, new int[] {65257, 65258, 65259, 65260}},	{64508, new int[] {64508, 64509, 64510, 64511}},	{65153, new int[] {65153, 65154, 65153, 65154}},
			{1575, new int[] {1575, 65166, 65165, 65166}},	{1576, new int[] {1576, 65168, 65169, 65170}},	{1662, new int[] {1662, 64343, 64344, 64345}},	{1578, new int[] {1578, 65174, 65175, 65176}},	{1579, new int[] {1579, 65178, 65179, 65180}},	{1580, new int[] {1580, 65182, 65183, 65184}},	{1670, new int[] {1670, 64379, 64380, 64381}},	{1581, new int[] {1581, 65186, 65187, 65188}},	{1582, new int[] {1582, 65190, 65191, 65192}},	{1583, new int[] {1583, 65194, 65193, 65194}},	{1584, new int[] {1584, 65196, 65195, 65196}},	{1585, new int[] {1585, 65198, 65197, 65198}},	{1586, new int[] {1586, 65200, 65199, 65200}},	{1688, new int[] {1688, 64395, 64394, 64395}},	{1587, new int[] {1587, 65202, 65203, 65204}},	{1588, new int[] {1588, 65206, 65207, 65208}},	{1589, new int[] {1589, 65210, 65211, 65212}},	{1590, new int[] {1590, 65214, 65215, 65216}},	{1591, new int[] {1591, 65218, 65219, 65220}},	{1592, new int[] {1592, 65222, 65223, 65224}},	{1593, new int[] {1593, 65226, 65227, 65228}},	{1594, new int[] {1594, 65230, 65231, 65232}},	{1601, new int[] {1601, 65234, 65235, 65236}},	{1602, new int[] {1602, 65238, 65239, 65240}},	{1705, new int[] {1705, 64399, 64400, 64401}},	{1711, new int[] {1711, 64403, 64404, 64405}},	{1604, new int[] {1604, 65246, 65247, 65248}},	{1605, new int[] {1605, 65250, 65251, 65252}},	{1606, new int[] {1606, 65254, 65255, 65256}},	{1608, new int[] {1608, 65262, 65261, 65262}},	{1607, new int[] {1607, 65258, 65259, 65260}},	{1740, new int[] {1740, 64509, 64510, 64511}},	{1570, new int[] {1570, 65154, 65153, 65154}},

			{65265, new int[] {65265, 65266, 65267, 65268}}, {65171, new int[] {65171, 65172, 65171, 65172}}, {65263, new int[] {65263, 65264, 65263, 65264}},	{65157, new int[] {65157, 65158, 65157, 65158}},	{65155, new int[] {65155, 65156, 65155, 65156}},	{65159, new int[] {65159, 65160, 65159, 65160}},	{65241, new int[] {65241, 65242, 65243, 65244}},	{65161, new int[] {65161, 65162, 65163, 65164}},
			{1610, new int[] {65265, 65266, 65267, 65268}}, {1577, new int[] {65171, 65172, 65171, 65172}}, {1609, new int[] {65263, 65264, 65263, 65264}},	{1572, new int[] {65157, 65158, 65157, 65158}},	{1571, new int[] {65155, 65156, 65155, 65156}},	{1573, new int[] {65159, 65160, 65159, 65160}},	{1603, new int[] {65241, 65242, 65243, 65244}},	{1574, new int[] {65161, 65162, 65163, 65164}},

		};
		
		public static Dictionary <int,int[]> boolStorage = new Dictionary<int, int[]> ()
		{
			{65165, new int[] {0, 1}}, 	{65167, new int[] {1, 1}}, 	{64342, new int[] {1, 1}}, 	{65173, new int[] {1, 1}}, 	{65177, new int[] {1, 1}}, 	{65181, new int[] {1, 1}}, 	{64378, new int[] {1, 1}}, 	{65185, new int[] {1, 1}}, 	{65189, new int[] {1, 1}}, 	{65193, new int[] {0, 1}}, 	{65195, new int[] {0, 1}}, 	{65197, new int[] {0, 1}}, 	{65199, new int[] {0, 1}}, 	{64394, new int[] {0, 1}}, 	{65201, new int[] {1, 1}}, 	{65205, new int[] {1, 1}}, 	{65209, new int[] {1, 1}}, 	{65213, new int[] {1, 1}}, 	{65217, new int[] {1, 1}}, 	{65221, new int[] {1, 1}}, 	{65225, new int[] {1, 1}}, 	{65229, new int[] {1, 1}}, 	{65233, new int[] {1, 1}}, 	{65237, new int[] {1, 1}}, 	{64398, new int[] {1, 1}}, 	{64402, new int[] {1, 1}}, 	{65245, new int[] {1, 1}}, 	{65249, new int[] {1, 1}}, 	{65253, new int[] {1, 1}}, 	{65261, new int[] {0, 1}}, 	{65257, new int[] {1, 1}}, 	{64508, new int[] {1, 1}}, 	{65153, new int[] {0, 1}}, 
			{1575, new int[] {0, 1}}, 	{1576, new int[] {1, 1}}, 	{1662, new int[] {1, 1}}, 	{1578, new int[] {1, 1}}, 	{1579, new int[] {1, 1}}, 	{1580, new int[] {1, 1}}, 	{1670, new int[] {1, 1}}, 	{1581, new int[] {1, 1}}, 	{1582, new int[] {1, 1}}, 	{1583, new int[] {0, 1}}, 	{1584, new int[] {0, 1}}, 	{1585, new int[] {0, 1}}, 	{1586, new int[] {0, 1}}, 	{1688, new int[] {0, 1}}, 	{1587, new int[] {1, 1}}, 	{1588, new int[] {1, 1}}, 	{1589, new int[] {1, 1}}, 	{1590, new int[] {1, 1}}, 	{1591, new int[] {1, 1}}, 	{1592, new int[] {1, 1}}, 	{1593, new int[] {1, 1}}, 	{1594, new int[] {1, 1}}, 	{1601, new int[] {1, 1}}, 	{1602, new int[] {1, 1}}, 	{1705, new int[] {1, 1}}, 	{1711, new int[] {1, 1}}, 	{1604, new int[] {1, 1}}, 	{1605, new int[] {1, 1}}, 	{1606, new int[] {1, 1}}, 	{1608, new int[] {0, 1}}, 	{1607, new int[] {1, 1}}, 	{1740, new int[] {1, 1}}, 	{1570, new int[] {0, 1}}, 

			{65265, new int[] {1, 1}}, 	{65171, new int[] {0, 1}}, 	{65263, new int[] {0, 1}}, 	{65157, new int[] {0, 1}}, 	{65155, new int[] {0, 1}}, 	{65159, new int[] {0, 1}}, 	{65241, new int[] {1, 1}}, 	{65161, new int[] {1, 1}}, 
			{1610, new int[] {1, 1}}, 	{1577, new int[] {0, 1}}, 	{1609, new int[] {0, 1}}, 	{1572, new int[] {0, 1}}, 	{1571, new int[] {0, 1}}, 	{1573, new int[] {0, 1}}, 	{1603, new int[] {1, 1}}, 	{1574, new int[] {1, 1}}, 

		};

		public static string faConvert(this string input)
		{
			string outStr = "";
			using (StringReader reader = new StringReader(input))
			{
				string line;
				bool more = false;
				while ((line = reader.ReadLine()) != null)
				{
					if (more) outStr += "\n";
					outStr += faConvertLine(line);
					more = true;
				}
			}

            char[] eng = { '0', '1', '2', '3', '4', '5', '6', '7', '8', '9' };
            char[] fa = { '٠', '١', '٢', '٣', '٤', '٥', '٦', '٧', '٨', '٩' };

            for (int i = 0; i < eng.Length; i++)
            {
                outStr = outStr.Replace(eng[i], fa[i]);
            }

            return outStr;
		}
		
		public static string faConvertLine(string input)
		{
			char[] charTemp = input.ToCharArray ();
			
			char chT;
			List<char> listT = new List<char>();
			List<char> listF = new List<char>();
			for (int i=0; i < charTemp.Length; i++)
			{
				if (IsFarsi(charTemp[i]))
				{
					chT = ConvertChar((i==0)?0:Convert.ToInt32(charTemp[i-1]),
					                  Convert.ToInt32(charTemp[i]),
					                  (i==charTemp.Length-1)?0:Convert.ToInt32(charTemp[i+1]));
					
					listT.Reverse();
					listF.AddRange(listT);
					listF.Add(chT);
					
					listT.Clear();
				}
				else
				{
					listT.Add(charTemp[i]);
				}
			}
			
			listT.Reverse();
			listF.AddRange(listT);
			listF.Reverse();
			return new string (listF.ToArray());
		}
		
		//This method just work with basic type letter - this method use next and previous letter to make decision
		//for change the type of letter
		public static char ConvertChar(int prev, int curr, int next)
		{
			int pr, ne;
			int[] tmp; int[] ctmp;
			
			pr = boolStorage.TryGetValue (prev, out tmp) ? tmp[0] : 0;
			
			ne = boolStorage.TryGetValue (next, out tmp) ? tmp[1] : 0;
			
			if (charStorage.TryGetValue(curr, out ctmp))
				return Char.ConvertFromUtf32(ctmp[pr + (ne * 2)])[0];
			return Char.ConvertFromUtf32(curr)[0];
		}

		//this method convert the last convereted letter to the basic type of it for useing in convertChar method
		public static int ConvertToBasic(int number)
		{
			return basicCharStorage[number];
		}
		
		public static bool IsFarsi(char ch)
		{
			int ch16 = Convert.ToInt32(ch);
			if ((ch16 >= 1536 && ch16 <= 1791) || (ch16 >= 65136 && ch16 <= 65279))
				return true;
			
			return false;
		}
        public static string multiLanguageText(string englishText, string persianText)
        {
            if (startLanguage.language == sysLang.Persian)
            {
                return Farsi.faConvert(persianText);
            }
            else
            {
                return englishText;
            }
        }
        public static string multiLanguageNumber(int num)
        {
            if (startLanguage.language == sysLang.Persian)
            {
                return Farsi.faConvert(num.ToString());
            }
            else
            {
                return num.ToString();
            }
        }
	}
}
