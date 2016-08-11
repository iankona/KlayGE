# 缘起：
2016年08月11号
* 	本分支从gongminmin/KlayGE develop分支2016年08月10号版本fork而来，源地址：https://github.com/gongminmin/KlayGE。
* 	本分支主要是学习游戏引擎，意味着本分支将被修改的面目全非，乃至无法编译与运行。
* 	如有意外见到此分支的编程者意外的想推送修改给我，请您不必如此，请您前往本分支的上级分支gongminmin/KlayGE建立分支，并推送您的修改。

# 幻想目标：
* 	实现mips平台版本，并深度优化。

# 长期目标：
	学习本引擎架构、库及其实现。
	
# 修剪内容：
* 	1.去除跨平台架构，仅保留linux实现。
* 	2.去除其他编译器，仅保留clang编译器。
* 	3.底层实现去除其他API，仅使用VulkanAPI。
* 	4.工程管理去除cmake，使用Makefile管理。
* 	5.编写引擎架构文档，API使用文档，一个完整大型3D游戏实现（单地图）及其完整说明。
* 	6.

# 近期目标：
* 	1.熟悉引擎架构，分清架构实现和库实现，画架构框图（xmind）、库调用图（freemind）和依赖层图（freemind）。
* 	2.编写各层的接口API文档。
* 	3.底层实现去除其他API，保留OpenGLAPI和D3D12（待定）。

# 原文档：

# KlayGE
KlayGE is a cross-platform open source game engine with plugin-based architecture. It's started since 2003. The explicit goal of KlayGE is: to arm your engine with cutting-edge technology. It provides a framework to make game development, testing, porting as simple as possible. 

# Getting started
Before compiling KlayGE, Python 2.7+ and CMake 2.8.6+ must be installed first. Then you can run build_all.py to build KlayGE, its Samples, Tools, etc. For details, please see http://www.klayge.org/wiki/index.php/Installation.

# Platforms
KlayGE can be developed on those platforms, with no limitation.
* Windows desktop
* MacOSX
* Linux

KlayGE can be deployed and run on those runtime platforms. Some of them doesn't supports all plugins. Details in http://www.klayge.org/wiki/index.php/Platforms
* Windows desktop
* Windows tablet
* Windows phone
* Android
* Linux
* MacOSX
* iOS

# Contribute
As an open source project, KlayGE benefits greatly from both the volunteer work of helpful developers and good bug reports made by users. 

## Bug Reports & Feature Requests
If you've noticed a bug or have an idea that you'd like to see come real, why not work on it? Bug reports and feature requests are typically submitted to the issue tracker https://github.com/gongminmin/KlayGE/issues.

## Engineering Workflow
If you want contribute some code, please check http://www.klayge.org/wiki/index.php/Workflow_of_contributing_code before submitting a patch or pull request.

# Links
* Website: http://www.klayge.org
* Wiki: http://www.klayge.org/wiki
