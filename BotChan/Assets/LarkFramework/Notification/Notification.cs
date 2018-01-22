/*---------------------------------------------------------------
 * 作者：evesgf    创建时间：2016-8-2 10:19:07
 * 修改：evesgf    修改时间：2016-8-2 10:19:14
 *
 * 版本：V0.0.2
 * 
 * 描述：消息通知中心
 * 
 * TODO:消息体需要进行扩展
 ---------------------------------------------------------------*/

using UnityEngine;
using System;

namespace LarkFramework
{
    // 通知类的对象发送到接收通知的对象类型。
    // 这个类包含发送GameObject,通知的名称,并选择一个包含数据的哈希表。
    public class Notification
    {
        public Component sender;
        public String name;
        public object data;

        public Notification(Component aSender, String aName) { sender = aSender; name = aName; data = null; }
        public Notification(Component aSender, String aName, object aData) { sender = aSender; name = aName; data = aData; }
    }
}
