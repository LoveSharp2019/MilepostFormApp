﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cell.Interface
{
    public interface IDataPool
    {
        /// <summary>
        /// 注册一个项（单值）
        /// </summary>
        /// <param name="key">项名</param>
        /// <param name="itemType">项值的类型</param>
        /// <returns></returns>
        bool RegistItem(string key, Type itemType, object initValue);


        /// <summary>
        /// 是否包含一个(单值)项
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        bool ContainItem(string key);

        /// <summary>
        /// 移除一个项（单值）
        /// </summary>
        /// <param name="key"></param>
        object RemoveItem(string key);

        /// <summary>
        /// 获取所有已注册的单值项的名称
        /// </summary>
        string[] AllItemKeys { get; }

        /// <summary>
        /// 获取单值项的值类型
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        Type GetItemType(string key);
        /// <summary>
        /// 设置一个单值项的值
        /// </summary>
        /// <param name="key"></param>
        /// <param name="value"></param>
        bool SetItemValue(string key, object value);

        /// <summary>
        /// 获取一个单值项的值
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        bool GetItemValue(string key, out object value);

        /// <summary>
        /// 注册一个列表项
        /// </summary>
        /// <param name="key">项名称</param>
        /// <param name="itemType">列表项元素的值类型</param>
        /// <returns></returns>
        bool RegistList(string key, Type itemType);

        /// <summary>
        /// 所有已注册的列表项的名称
        /// </summary>
        string[] AllListKeys { get; }

        /// <summary>
        /// 获取列表项元素值类型
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        Type GetListElementType(string key);

        /// <summary>
        /// 获取一个列表项并锁定（同步锁）
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>     
        object LockList(string key);

        /// <summary>
        /// 解除列表项的锁定
        /// </summary>
        /// <param name="key"></param>
        void UnlockList(string key);

        /// <summary>
        /// 修改列表项的值
        /// </summary>
        /// <param name="lstValue"></param>
        void SetList(string key, object lstValue);

        /// <summary>
        /// 获取列表项长度
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        int GetListCount(string key);

        /// <summary>
        /// 清空列表项
        /// </summary>
        /// <param name="key"></param>
        void ClearList(string key);

        /// <summary>
        /// 获取列表项的首元素（不删除）
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        object PeekList(string key);

        /// <summary>
        /// 向列表项的末尾添加一个元素
        /// </summary>
        /// <param name="key"></param>
        /// <param name="element"></param>
        /// <returns></returns>
        bool EnqueList(string key, object element);

        /// <summary>
        /// 获取列表项的首元素（并从队列中删除）
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        object DequeList(string key);

        /// <summary>
        /// 获取列表项的尾元素并从队列中删除（出栈操作）
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        object PopList(string key);

    }
}
