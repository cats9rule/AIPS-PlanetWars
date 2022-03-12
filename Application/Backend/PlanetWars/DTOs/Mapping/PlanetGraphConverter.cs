using System;
using System.Collections.Generic;
using System.Linq;
using AutoMapper;

namespace PlanetWars.DTOs.MappingProfiles
{
    public class PlanetGraphConverter : IValueConverter<string, Dictionary<int, List<int>>>
    {
        public Dictionary<int, List<int>> Convert(string sourceMember, ResolutionContext context)
        {
            List<string> entries = new List<string>(sourceMember.Split('|'));
            Dictionary<int, List<int>> dict = new Dictionary<int, List<int>>();
            foreach (string entry in entries)
            {
                string[] keyval = entry.Split(':');
                int key = Int32.Parse(keyval[0]);
                List<int> values = (new List<string>(keyval[1].Split(','))).Select(str => Int32.Parse(str)).ToList();
                dict.Add(key, values);
            }
            return dict;
        }
    }
}