# あやかしゲーム
こちらは”あやかしゲーム”のUnityプロジェクトです。


# Unityroomにて配信中
[こちら](https://unityroom.com/games/ayakashi_game)からブラウザ上でプレイできます。

## UnityEditor内でゲームを実行する場合
Unityエディタ内でゲームを実行するときはAssets/Scenes/InGameシーンから実行をお願いします。

Strix CloudのApplicationID等を保護する目的から、ApplicationIDとマスターホスト名を格納するScriptableObjectを以下の手順に沿って作成し、
ApplicationIDとマスターホスト名を発行して貼り付けてください。

1. Projectウィンドウの"+" -> CreateData -> StrixAppDataをクリックしてScriptableObjectを作成

![スクリーンショット 2024-08-03 2 08 45](https://github.com/user-attachments/assets/042be4ac-64c9-4c86-9405-ac8b46e46969)

2. StrixAppDataのApplicationIDとHostURLにそれぞれApplicationIDとマスターホスト名を転記してください。
   
![スクリーンショット 2024-08-03 2 17 26](https://github.com/user-attachments/assets/3d6c69d6-cc34-43a8-b78e-18d9c0465eb9)

3. シーン内にあるRoomJoinManagerオブジェクト -> Connect To Roomコンポーネント -> AppDataに先ほど作成したScriptableObjectを割り当ててください。
4. 
![スクリーンショット 2024-08-03 2 09 12](https://github.com/user-attachments/assets/61d09003-bacc-4283-bf22-338bb0fb5163)


# 操作方法
## プレイヤーの移動
右移動：Dキー, 画面左下の右ボタン

左移動：Aキー, 画面左下の左ボタン

ピースを落とす：Enter(Return)キー, 画面右下のボタンをクリック

# 使用アセット、フリー素材
[OtoLogic](https://otologic.jp/)

ゲーム内のSEとして使用

[効果音ラボ](https://soundeffect-lab.info/)

SEとして使用。
