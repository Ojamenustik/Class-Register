//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace ClassRegister
{
    using System;
    using System.Collections.Generic;
    
    public partial class OcenyUcznia
    {
        public long IdOcenyUcznia { get; set; }
        public long IdUcznia { get; set; }
        public long IdPrzedmiotu { get; set; }
        public long Ocena { get; set; }
        public string Dzien { get; set; }
    
        public virtual Klasa Klasa { get; set; }
        public virtual Przedmioty Przedmioty { get; set; }
    }
}
