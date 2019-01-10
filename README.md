# Ku.Core.CMS
基于.net core开发的内容管理系统

# 运行方法
<ol>
<li>拉取最新代码</li>
<li>修改Ku.Core.CMS.Data.Migrations、Ku.Core.CMS.Web.Backend、Ku.Core.CMS.WinService目录下的appsettings.json中的连接字符串（现只能mysql）和redis的连接字符串</li>
<li>命令行cd到Ku.Core.CMS.Data.Migrations目录下</li>
<li>执行：dotnet ef migrations add init</li>
<li>执行：dotnet ef database update</li>
<li>运行一遍Ku.Core.CMS.Data.Migrations项目</li>
<li>将docs\database\initdata.sql导入到mysql数据库（菜单数据）</li>
<li>运行Ku.Core.CMS.Web.Backend项目，用admin/123456就可以登陆后台了</li>
</ol>

## 使用组件及框架
<ol>
<li>.net core 2.1</li>
<li>EF core 2.1</li>
<li>Dapper</li>
<li>Layui</li>
<li>CAP</li>
<li>Redis</li>
<li>MYSQL（后续兼容MSSQL）</li>
<li>NLOG</li>
<li>Swagger</li>
</ol>
