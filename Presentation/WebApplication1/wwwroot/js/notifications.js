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
        <div class="notification-item ${!isRead ? "unread" : ""}">

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

async function loadNotifications() {

    try {

        const response =
            await fetch("/Notification/GetAll");

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

        unreadCount =
            notifications.filter(
                x => !(x.isRead ?? x.IsRead))
                .length;

        updateUnreadCounter();
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

        setTimeout(() =>
            toast.remove(), 300);

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

        unreadCount = 0;

        updateUnreadCounter();

        document
            .querySelectorAll(
                ".notification-item.unread")
            .forEach(x =>
                x.classList.remove(
                    "unread"));
    });

clearNotificationsButton
    ?.addEventListener(
        "click",
        () => {

            notificationList.innerHTML = `
                <div class="notification-empty">
                    Henüz bildirim bulunmuyor
                </div>
            `;

            unreadCount = 0;

            updateUnreadCounter();
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

window.connection.onreconnecting(() => {

    if (connectionIndicator) {

        connectionIndicator.dataset.state =
            "disconnected";
    }
});

window.connection.onreconnected(() => {

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
    })
    .catch(error =>
        console.error(
            "SignalR Error:",
            error));