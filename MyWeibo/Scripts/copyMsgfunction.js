function copyMsgfunction(msgId) {


    $.get("/Weibo/CopyMessagePartial", { msgId: msgId }, function (data) {

        $("#copyMsgDialog").html(data);
    });
    $("#copyMsgDialog").dialog("open");
    

}