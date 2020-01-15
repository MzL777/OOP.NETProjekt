﻿using System;
using System.Collections.Generic;

using System.Globalization;
using DAL.Model.Matches;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

using J = Newtonsoft.Json.JsonPropertyAttribute;

// <auto-generated />
//
// To parse this JSON data, add NuGet 'Newtonsoft.Json' then do:
//
//    using DAL.Models.Teams;
//
//    var teams = Team.FromJson(jsonString);

namespace DAL.Model.Teams
{
    public partial class Team
    {
        [J("id")] public long Id { get; set; }
        [J("country")] public string Country { get; set; }
        [J("alternate_name")] public object AlternateName { get; set; }
        [J("fifa_code")] public string FifaCode { get; set; }
        [J("group_id")] public long GroupId { get; set; }
        [J("group_letter")] public string GroupLetter { get; set; }

        public IEnumerable<Player> Players;

        public override bool Equals(object obj) => obj is Team other ? other.FifaCode.Equals(this.FifaCode) : obj is MatchingTeam othermt ? othermt.FifaCode.Equals(this.FifaCode) : false;
        public override int GetHashCode() => FifaCode.GetHashCode();
        public override string ToString() => $"{this.Country} ({this.FifaCode})";
    }


    public partial class Team
    {
        public static List<Team> FromJson(string json) => JsonConvert.DeserializeObject<List<Team>>(json, DAL.Model.Teams.Converter.Settings);
    }


    public static class Serialize
    {
        public static string ToJson(this List<Team> self) => JsonConvert.SerializeObject(self, DAL.Model.Teams.Converter.Settings);
    }

    internal static class Converter
    {
        public static readonly JsonSerializerSettings Settings = new JsonSerializerSettings
        {
            MetadataPropertyHandling = MetadataPropertyHandling.Ignore,
            DateParseHandling = DateParseHandling.None,
            Converters =
            {
                new IsoDateTimeConverter { DateTimeStyles = DateTimeStyles.AssumeUniversal }
            },
        };
    }
}
