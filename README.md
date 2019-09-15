![](https://github.com/gaufung/DataStructureDotNet/workflows/Data%20Structure%20Tests/badge.svg)

# Data Structure
Data Structure in C#  Language（数据结构 C#）

# Introduction
+ 通过C#语言完成数据结构（主要包含：向量、链表、树、图）
+ 参考书《数据结构 C++版》清华大学邓俊辉老师编写 [封面](http://img36.ddimg.cn/39/12/22526796-1_u_1.jpg)
+ 网络公开课地址:[MOOC](https://www.xuetangx.com/)

# Description
+ 将书中中所有数据结构和算法，使用C#语言重写，整本书以一个解决方案的形式存放，每种数据结构均以一个工程的形式组织起来。
+ 采用TDD 方式，使用的测试工具为NUnit
+ 数据结构采用泛型设计

# Content

## Abstract Data Type

内容 | 说明
---|---
Vector | C++ STL 中Vector模板类
List | .Net Framework中的List<T>泛型
Stack | .Net Framework中的Stack<T>泛型
Queue | .Net Framework中的Queue<T>泛型
Binary Tree | 二叉树实现
Graph | 图 
Dictionary | .Net Framework中Dictionary<TK,TV>泛型  
SkipList | 跳转列表  
Strings | .net framework中的string(部分功能)

## Algorithm
- [x] 栈
    - 达式求值和逆波兰表达式(Reverse Polish Natation)
    - N 皇后排列问题
- [x] 二叉树遍历
    - 递归版先序遍历，中序遍历和后序遍历   
    - 迭代版先序遍历，中序遍历和后序遍历  
    - Huffman编码树
- [x] 图搜索
    - 广度优先搜索  
    - 深度优先搜索 
    - 拓扑排序 
    - 最小支撑树
        - Prim算法
        - Kruskal算法
    - 优先级搜索
    - 最短路径
- [x] 查找平衡树
  	- Avl树(Avl Tree)
  	- 伸展树(Splay Tree)
  	- B树(B-Tree)  
- [ ] 红黑树
- [x] 二叉堆(Heap)
- [x] 串
    - KMP算法
- [x] 排序算法
    - 选择排序
    - 插入排序
    - 冒泡排序
    - 希尔排序
    - 归并排序
    - 快速排序
    - 堆排序
- [x] 查找
    - 众数查找
    - 第kth元素查找
