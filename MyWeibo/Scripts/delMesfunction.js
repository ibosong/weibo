function delMsgfunction(msgId) {
    $.Zebra_Dialog("确定删除？", {
        'modal': true,
        'type': false,
        'width': 300,
        'buttons': ['确定', '取消'],
        'onClose': function (caption) {
            if (caption == '确定') {
                $.get("/Weibo/DelMessage", { msgId: msgId }, function (data) {
                    $.Zebra_Dialog(data.message, {
                        'buttons': false,
                        'modal': true,
                        'width': 200,
                        'type': false,
                        'auto_close': 700
                    });
                });
            }
        }

    });
}