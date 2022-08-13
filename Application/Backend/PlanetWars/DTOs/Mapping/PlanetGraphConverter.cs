using System;
using System.Collections.Generic;
using System.Linq;
using AutoMapper;

namespace PlanetWars.DTOs.MappingProfiles
{
    public class PlanetGraphConverter : IValueConverter<string, Dictionary<string, List<int>>>
    {
        public Dictionary<string, List<int>> Convert(string sourceMember, ResolutionContext context)
        {
            List<string> entries = new List<string>(sourceMember.Split('|'));
            Dictionary<string, List<int>> dict = new Dictionary<string, List<int>>();
            foreach (string entry in entries)
            {
                string[] keyval = entry.Split(':');
                string key = keyval[0];
                List<int> values = (new List<string>(keyval[1].Split(','))).Select(str => Int32.Parse(str)).ToList();
                dict.Add(key, values);
            }
            return dict;
        }
    }
}