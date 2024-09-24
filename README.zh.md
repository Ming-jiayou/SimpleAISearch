简体中文|[English](./README.md) 

# SimpleAISearch✨

# 基于C# Semantic Kernel 与 DuckDuckGo实现简单的AI搜索✨

最近AI搜索很火爆，有Perplexity、秘塔AI、MindSearch、Perplexica、memfree、khoj等等。

在使用大语言模型的过程中，或许你也遇到了这种局限，就是无法获取网上最新的信息，导致回答的内容不是基于最新的信息，为了解决这个问题，可以通过LLM+搜索引擎的方式实现。

以我之前开源的一个简单项目为例，如果直接问一般的大语言模型是不知道的，如下所示：

![image-20240920103257679](https://mingupupup.oss-cn-wuhan-lr.aliyuncs.com/imgs/image-20240920103257679.png)

对比可以联网的回答：

Perplexity

![image-20240920103503743](https://mingupupup.oss-cn-wuhan-lr.aliyuncs.com/imgs/image-20240920103503743.png)

khoj

![image-20240920103739835](https://mingupupup.oss-cn-wuhan-lr.aliyuncs.com/imgs/image-20240920103739835.png)

Kimi

![image-20240920103933071](https://mingupupup.oss-cn-wuhan-lr.aliyuncs.com/imgs/image-20240920103933071.png)

那么我们如何自己实现类似的效果呢？

先来看看自己实现的效果：

![image-20240920104451845](https://mingupupup.oss-cn-wuhan-lr.aliyuncs.com/imgs/image-20240920104451845.png)

源码GitHub地址：https://github.com/Ming-jiayou/SimpleAISearch

如果对此感兴趣的话，就可以继续往下阅读。

## 实现思路

本质上就是LLM+搜索引擎。

首先需要能够实现函数调用功能，在之前的文章中已经有所说明。主要介绍一下实现思路，源码已经开源，感兴趣的话可以自己去看下具体代码。

首先在插件中添加调用搜索引擎的代码，我这里搜索引擎选用的是DuckDuckGo。

开始执行时，LLM会判断需要调用这个函数，并且参数是问题：

![image-20240920105218166](https://mingupupup.oss-cn-wuhan-lr.aliyuncs.com/imgs/image-20240920105218166.png)

这个函数如下所示：

![image-20240920105254572](https://mingupupup.oss-cn-wuhan-lr.aliyuncs.com/imgs/image-20240920105254572.png)

搜索引擎会找到相关内容：

![image-20240920105409114](https://mingupupup.oss-cn-wuhan-lr.aliyuncs.com/imgs/image-20240920105409114.png)

让LLM根据获取到的这些信息给出回答：

![image-20240920105518735](https://mingupupup.oss-cn-wuhan-lr.aliyuncs.com/imgs/image-20240920105518735.png)

目前是经过总结之后显示在界面上，也可以修改为不经过总结的。

以上就是实现的一个简单思路。

## 快速体验

**通过源码构建**

和之前的LLM项目一样，只需appsettings.example.json修改为appsettings.json选择你使用的平台并填入API Key即可。

**直接体验**

我已经在github上发布了两个版本一个依赖框架，一个不依赖框架：

![image-20240920113656942](https://mingupupup.oss-cn-wuhan-lr.aliyuncs.com/imgs/image-20240920113656942.png)

下载解压之后，在appsettings中填入你的api key即可使用。

