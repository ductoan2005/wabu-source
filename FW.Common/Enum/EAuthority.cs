using System;

namespace FW.Common.Enum
{
    [Serializable]
    public enum EAuthority : byte
    {
        Root = 0,
        Administrator = 1,
        Investor = 2,
        Contractor = 3
    }
}
