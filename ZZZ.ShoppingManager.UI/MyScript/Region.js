$(function () {
    $("#btnRegin").click(function () {
        var UPwd = $("#UPwd").val();
        var UPwdAgain = $("#UPwdAgain").val();
        if (UPwd != UPwdAgain) {
            alert("两次密码必须一致");
            return false;
        }

    });
});