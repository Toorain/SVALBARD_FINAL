<%@ Page Title="" Language="C#" AutoEventWireup="true" CodeBehind="NotADUser.aspx.cs" Inherits="WebApplication1.NotADUser" %>

<section style="display: flex; justify-content: center; align-items: center; min-height: 90vh">
    <div style="text-align: center">
        <h1>Votre profil n'exite pas dans l'Active Directory sur l'application</h1>
        <h3>Rapprochez-vous de votre DSI pour en savoir plus</h3>
        <h3>Infos à communiquer à votre DSI : </h3>
        <ul style="text-align: left; margin-top: 5%;">
            <li>SERVER : CUBA</li>
            <li>DB : PATRIMOINE</li>
            <li>Table : AD_CCIT</li>
            <li>Problème : L'utilisateur n'existe pas dans la table AD_CCIT</li>
        </ul>
    </div>
</section>
