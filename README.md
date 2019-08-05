# 群益 API 基礎應用

### 使用 C Sharp 建立一個 Windows Form App

API 版本： 2.13.17  [群益 API 下載頁面](https://www.capital.com.tw/Service2/download/API.asp)

一開始請依照 API 文件說明，申請 API 使用權限並註冊元件。

開啟專案後要加入參考，我是將 API 檔案放在 C:\SKCOM ，找到路徑後加入 SKCOM.dll 這個參考。

以下是這個基礎應用的截圖，輸入帳號跟密碼後按下登入，會依序取得連線資訊，接著會開始接收大台( TX00 )的即時 tick 資訊。

![intro image](https://raw.githubusercontent.com/itisjoe/CapitalAPI_Basic/master/intro.jpg)
