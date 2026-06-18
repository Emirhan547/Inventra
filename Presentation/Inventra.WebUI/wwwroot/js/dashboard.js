window.connection.on(
    "DashboardUpdated",
    () => {

        if (
            window.location.pathname === "/" ||
            window.location.pathname === "/Dashboard" ||
            window.location.pathname === "/Dashboard/Index"
        ) {
            location.reload();
        }
    });