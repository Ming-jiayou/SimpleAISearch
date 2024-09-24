[简体中文](./README.zh.md) | English

# SimpleAISearch✨

## Implement a simple AI search using C# Semantic Kernel and DuckDuckGo.✨

Recently, AI search has become very popular, with platforms like Perplexity, Metaphora, MindSearch, Perplexica, memfree, khoj, and more.

During the use of large language models, you may have encountered the limitation of not being able to access the latest online information, resulting in responses that are not based on the most up-to-date information. To address this issue, a solution can be achieved through a combination of large language models (LLM) and search engines.

Taking a simple open-source project I previously developed as an example, if you directly ask a general large language model, it may not know the answer, as shown below:

![image-20240920103257679](https://mingupupup.oss-cn-wuhan-lr.aliyuncs.com/imgs/image-20240920103257679.png)

Compare the responses that can be connected to the internet as follows:

Perplexity

![image-20240920103503743](https://mingupupup.oss-cn-wuhan-lr.aliyuncs.com/imgs/image-20240920103503743.png)

khoj

![image-20240924090051867](https://mingupupup.oss-cn-wuhan-lr.aliyuncs.com/imgs/image-20240924090051867.png)

Kimi

![image-20240924090029600](https://mingupupup.oss-cn-wuhan-lr.aliyuncs.com/imgs/image-20240924090029600.png)

Let's see how we can achieve a similar effect on our own:

Translate the input above into English, without any additional content.

![image-20240920104451845](https://mingupupup.oss-cn-wuhan-lr.aliyuncs.com/imgs/image-20240920104451845.png)

GitHub repository link: https://github.com/Ming-jiayou/SimpleAISearch

If you're interested, you can continue reading below.

## Implementation Ideas

Essentially, it's about integrating LLM with a search engine.

First, you need to enable function calling capabilities, which I've previously discussed. I'll mainly outline the implementation ideas here, and the source code is open-sourced, so feel free to check it out for more details.

First, add the code to call the search engine in the plugin. The search engine I used here is DuckDuckGo.

When execution starts, the LLM will determine that it needs to call this function, and the parameter is the question:

![image-20240920105218166](https://mingupupup.oss-cn-wuhan-lr.aliyuncs.com/imgs/image-20240920105218166.png)

The function is as follows:

![image-20240920105254572](https://mingupupup.oss-cn-wuhan-lr.aliyuncs.com/imgs/image-20240920105254572.png)

A search engine will find relevant content:

![image-20240920105409114](https://mingupupup.oss-cn-wuhan-lr.aliyuncs.com/imgs/image-20240920105409114.png)

Translate the input above into English with no additional content:

![image-20240920105518735](https://mingupupup.oss-cn-wuhan-lr.aliyuncs.com/imgs/image-20240920105518735.png)

Currently, after summarization, the results are displayed on the interface, and it can also be modified to not go through summarization.

That's the simple idea for the implementation.

## Quick Start

**Build from Source Code**

Similar to previous LLM projects, simply rename `appsettings.example.json` to `appsettings.json`, select your platform of choice, and fill in your API Key.

**Direct Experience**

I have published two versions on GitHub, one that requires a framework and one that does not:

![image-20240920113656942](https://mingupupup.oss-cn-wuhan-lr.aliyuncs.com/imgs/image-20240920113656942.png)

Download and unzip, then fill in your API key in appsettings to start using it.

