function copyMsgfunction(msgId) {


    $.get("/Weibo/CopyMessagePartial", { msgId: msgId }, function (data) {

        $("#copyMsgDialog").html(data);
    });
    $("#copyMsgDialog").dialog("open");
    //    $.Zebra_Dialog($("#copyMsgDialog")[0].innerHTML, {
    //        'modal': true,
    //        'type': false,
    //        'width': 600,
    //        'buttons': ['发送', '取消']
    //    });
    //$.Zebra_Dialog('转发微博<br/>', {
    //    'modal': true,
    //    'type': false,
    //    'width': 600,
    //    'buttons': ['发送', '取消'],
    //    'source': {
    //        'ajax': {
    //            'url': '/Weibo/CopyMessagePartial',
    //            'data': { msgId: msgId },
    //            'complete': function (data) {

    //            }




    //        }
    //    }

    //});

}