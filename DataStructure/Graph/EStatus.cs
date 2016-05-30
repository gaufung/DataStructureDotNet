using System;

namespace Sequence
{
    /// <summary>
    /// 边状态
    /// </summary>
    [Serializable]
    public enum EStatus
    {
        /// <summary>
        /// 无定向边
        /// </summary>
        Undetermined,
        /// <summary>
        /// 支撑树构成的边
        /// </summary>
        Tree,
        /// <summary>
        /// 跨边
        /// </summary>
        Cross,
        /// <summary>
        /// 前向边
        /// </summary>
        Forward,
        /// <summary>
        /// 后退边
        /// </summary>
        Backward
    }
}
