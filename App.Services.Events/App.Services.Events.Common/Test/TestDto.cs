﻿using ProtoBuf;

namespace App.Services.Events.Common.Test
{
    [ProtoContract]
    [ProtoInclude(2, typeof(TestDetailedDto))]
    public class TestDto
    {
        [ProtoMember(1)]
        public string Id { get; set; }

        [ProtoMember(2)]
        public string SomePublicProeprty { get; set; }
    }
}