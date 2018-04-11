<%@ Application Language="C#" %>
<%@ Import Namespace="Website" %>
<%@ Import Namespace="System.Web.Optimization" %>
<%@ Import Namespace="System.Web.Routing" %>
<%@ Import Namespace="eBike.System.BLL.Security" %>

<script runat="server">

    void Application_Start(object sender, EventArgs e)
    {
        RouteConfig.RegisterRoutes(RouteTable.Routes);
        BundleConfig.RegisterBundles(BundleTable.Bundles);

        var rolemgr = new RoleManager();
        rolemgr.AddDefaultRoles();

        var usermgr = new UserManager();
        usermgr.AddWebMaster();
        usermgr.AddTeamMembers();
        usermgr.AddDefaultUsers();
    }

</script>
