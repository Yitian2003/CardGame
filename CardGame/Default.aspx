<%@ Page Title="Home Page" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="CardGame._Default" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <link href="Content/default.css" rel="stylesheet" />
</asp:Content>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">

    <div id="game_table">
        <div class="title">Computer Player</div>
        <div style="height: 170px;">
            <span class="card_hoder"></span><span class="card_hoder"></span><span class="card_hoder"></span><span class="card_hoder"></span>
            <span class="card_hoder"></span><span class="card_hoder"></span><span class="card_hoder"></span><span class="card_hoder"></span>
            <span class="card_hoder"></span><span class="card_hoder"></span>
       </div>
       <div class="title">You</div>
        <div style="height: 170px;">
            <span class="card_hoder"></span><span class="card_hoder"></span><span class="card_hoder"></span><span class="card_hoder"></span>
            <span class="card_hoder"></span><span class="card_hoder"></span><span class="card_hoder"></span><span class="card_hoder"></span>
            <span class="card_hoder"></span><span class="card_hoder"></span>
       </div>
        <div style="text-align: center;"><div class="btn" onclick="hit();">Hit Me</div></div>
    </div>

    <input type="hidden" id="Action" name="Action" />

    <script type="text/javascript">
        function hit() {
            $.ajax({
                type: 'POST',
                url: "/Services/WebService.asmx/HitMe",
                contentType: "application/json; charset=utf-8",
                success: function (r) {
                    post_back_data(r);
                }
            })
        }
        function new_game() {
            $.ajax({
                type: 'POST',
                url: "/Services/WebService.asmx/NewGame",
                contentType: "application/json; charset=utf-8",
                success: function (r) {
                    post_back_data(r);
                }
            })
        }
        function pass() {
            $.ajax({
                type: 'POST',
                url: "/Services/WebService.asmx/Pass",
                contentType: "application/json; charset=utf-8",
                success: function (r) {
                    post_back_data(r);
                }
            })
        }
        function post_back_data(r) {
            $("#game_table").html(r.d);
        }
    </script>
</asp:Content>
