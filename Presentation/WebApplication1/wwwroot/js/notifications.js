const notificationButton =
    document.getElementById("notificationButton");

const notificationPanel =
    document.getElementById("notificationPanel");

const notificationList =
    document.getElementById("notificationList");

const notificationCount =
    document.getElementById("notificationCount");

const connectionIndicator =
    document.getElementById("connectionIndicator");

const clearNotificationsButton =
    document.getElementById("clearNotifications");

const toastContainer =
    document.getElementById("toastContainer");

let unreadCount = 0;

window.connection =
    new signalR.HubConnectionBuilder()
        .withUrl(
            "https://localhost:7041/hubs/notifications",
            {
                accessTokenFactory: () =>
                    window.accessToken
            })
        .withAutomaticReconnect()
        .build();

function getNotificationTypeClass(type) {

    switch (type) {

        case "CriticalStock":
            return "notification-type-danger";

        case "PurchaseOrderApproved":
            return "notification-type-primary";

        case "PurchaseOrderCompleted":
            return "notification-type-success";

        case "StockIn":
            return "notification-type-success";

        default:
            return "notification-type-neutral";
    }
}

function createNotificationItem(notification) {

    const id =
        notification.id ??
        notification.Id;

    const title =
        notification.title ??
        notification.Title;

    const message =
        notification.message ??
        notification.Message;

    const type =
        notification.type ??
        notification.Type;

    const createdAt =
        notification.createdAt ??
        notification.CreatedAt;

    const isRead =
        notification.isRead ??
        notification.IsRead;

    const typeClass =
        getNotificationTypeClass(type);

    return `
        <div
            class="notification-item ${!isRead ? "unread" : ""}"
            data-id="${id}">

            <div class="notification-type ${typeClass}">
                !
            </div>

            <div class="notification-content">

                <strong>
                    ${title}
                </strong>

                <p>
                    ${message}
                </p>

                <time>
                    ${new Date(createdAt)
            .toLocaleString("tr-TR")}
                </time>

            </div>

        </div>
    `;
}

async function loadUnreadCount() {

    try {

        const response =
            await fetch(
                "/Notification/GetUnreadCount");

        if (!response.ok)
            return;

        unreadCount =
            await response.json();

        updateUnreadCounter();
    }
    catch (error) {

        console.error(
            "Unread Count Error:",
            error);
    }
}

async function loadNotifications() {

    try {

        const response =
            await fetch(
                "/Notification/GetAll");

        if (!response.ok) {

            throw new Error(
                "Bildirimler alınamadı");
        }

        const notifications =
            await response.json();

        notificationList.innerHTML = "";

        if (!notifications ||
            notifications.length === 0) {

            notificationList.innerHTML = `
                <div class="notification-empty">
                    Henüz bildirim bulunmuyor
                </div>
            `;

            return;
        }

        notifications.forEach(notification => {

            notificationList.insertAdjacentHTML(
                "beforeend",
                createNotificationItem(
                    notification));
        });
    }
    catch (error) {

        console.error(
            "Notification Load Error:",
            error);
    }
}

function showToast(notification) {

    if (!toastContainer)
        return;

    const type =
        notification.type ??
        notification.Type;

    const title =
        notification.title ??
        notification.Title;

    const message =
        notification.message ??
        notification.Message;

    const typeClass =
        getNotificationTypeClass(type)
            .replace(
                "notification-type",
                "notification-toast");

    const toast =
        document.createElement("div");

    toast.className =
        `notification-toast ${typeClass}`;

    toast.innerHTML = `
        <strong>
            ${title}
        </strong>

        <span>
            ${message}
        </span>
    `;

    toastContainer.appendChild(
        toast);

    requestAnimationFrame(() =>
        toast.classList.add("show"));

    setTimeout(() => {

        toast.classList.remove("show");

        setTimeout(
            () => toast.remove(),
            300);

    }, 5000);
}

function updateUnreadCounter() {

    if (!notificationCount)
        return;

    notificationCount.innerText =
        unreadCount;

    notificationCount.style.display =
        unreadCount > 0
            ? "flex"
            : "none";
}

notificationButton?.addEventListener(
    "click",
    () => {

        notificationPanel
            ?.classList
            .toggle("d-none");

        notificationButton.setAttribute(
            "aria-expanded",
            notificationPanel
                ?.classList
                .contains("d-none")
                ? "false"
                : "true");
    });

notificationList?.addEventListener(
    "click",
    async e => {

        const item =
            e.target.closest(
                ".notification-item");

        if (!item)
            return;

        if (!item.classList.contains(
            "unread"))
            return;

        const id =
            item.dataset.id;

        try {

            await fetch(
                `/Notification/MarkAsRead?id=${id}`,
                {
                    method: "PUT"
                });

            item.classList.remove(
                "unread");

            await loadUnreadCount();
        }
        catch (error) {

            console.error(
                "Mark As Read Error:",
                error);
        }
    });

clearNotificationsButton
    ?.addEventListener(
        "click",
        async () => {

            try {

                await fetch(
                    "/Notification/MarkAllAsRead",
                    {
                        method: "PUT"
                    });

                document
                    .querySelectorAll(
                        ".notification-item.unread")
                    .forEach(x =>
                        x.classList.remove(
                            "unread"));

                await loadUnreadCount();
            }
            catch (error) {

                console.error(
                    "Mark All Read Error:",
                    error);
            }
        });

window.connection.on(
    "ReceiveNotification",
    notification => {

        console.log(
            "Notification Received:",
            notification);

        unreadCount++;

        updateUnreadCounter();

        const empty =
            notificationList.querySelector(
                ".notification-empty");

        if (empty) {
            empty.remove();
        }

        notificationList.insertAdjacentHTML(
            "afterbegin",
            createNotificationItem(
                notification));

        showToast(notification);
    });

window.connection.onreconnecting(
    () => {

        if (connectionIndicator) {

            connectionIndicator.dataset.state =
                "disconnected";
        }
    });

window.connection.onreconnected(
    () => {

        if (connectionIndicator) {

            connectionIndicator.dataset.state =
                "connected";
        }
    });

window.connection.start()
    .then(async () => {

        console.log(
            "SignalR Connected");

        if (connectionIndicator) {

            connectionIndicator.dataset.state =
                "connected";
        }

        await loadNotifications();

        await loadUnreadCount();
    })
    .catch(error =>
        console.error(
            "SignalR Error:",
            error));