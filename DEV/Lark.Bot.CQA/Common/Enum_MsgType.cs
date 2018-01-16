using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/// <summary>
/// 消息来源类型
/// </summary>
public enum Enum_MsgType
{
    /// <summary>
    /// 私聊
    /// </summary>
    PrivateMsg=0,
    /// <summary>
    /// 群聊
    /// </summary>
    GroupMsg=1,
    /// <summary>
    /// 群成员私聊
    /// </summary>
    PrivateGroup=2
}
