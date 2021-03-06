﻿namespace TIGERConverters.RecordTypes
{
    // ReSharper disable InconsistentNaming
    public class RecordType6
    {
        public int TLID	{ get; set; }       // TIGER/Line® ID, Permanent 1-Cell Number
        public int RTSQ	{ get; set; }       // Record Sequence Number
        public string FRADDL { get; set; }  // Start Address, Left
        public string TOADDL { get; set; }  // End Address, Left
        public string FRADDR { get; set; }  // Start Address, Right
        public string TOADDR { get; set; }  // End Address, Right
    }
    // ReSharper restore InconsistentNaming
}
