<div align="center">

# Multidimenzionální absťák
> a gamebook by @toncekr & @steveruu  

**[Figma](https://www.figma.com/design/c06SWKpvasclNtKCNU2pQE/MDAGamebook?node-id=0-1&t=JM0Hsiub1j8QwFeF-1)** · [Notion](https://www.notion.so/steveruu/Multidimenzion-ln-abs-k-14492fa18d8f80ed82cfe8459281f9a7?pvs=4) · [Experimental branch](https://github.com/minjiyalabs/mda)

</div>

## Krátké shnrutí příběhu
Kdysi dávno ti tvůj starý kamarád přivezl záhadnou láhev od neznámého obchodníka. Šlo o tajemný nápoj, který vypadal jako směs čehosi fialového a duhového hvězdného prachu. Už první doušek tě katapultoval do neznámých dimenzí. Euforie, kterou vyvolával, byla nepopsatelná: cítil ses, jako bys mohl létat mezi hvězdami, procházet zdmi a rozmlouvat s dávno zapomenutými bohy. Tvůj život se najednou zdál být plný barev, melodií a nekonečných možností.

Jenže pak… nápoj došel.

S každým dnem bez něj ses cítil prázdnější. A co hůř, tvůj kamarád, který ti onu kouzelnou láhev přivezl, zmizel jako pára nad hrncem. Nezanechal žádné stopy, žádné vysvětlení. Jediné, co jsi věděl, bylo, že ten nápoj nemohl pocházet z této planety. Byl to dar z hvězd.

Rozhodl ses: najdeš ten nápoj, i kdybys měl procestovat celý vesmír.
## Vytvořte:
vlastní implementaci aplikace realizující Gamebook tak, aby celá hra šla hrát, aby vypadala jako gamebook stylu odpovídají tématu gamebooku a aby byla RPG - tedy, aby v ní hráč měl možnost volby, vývoj postavy, změnu stavu herního světa.

### Návrh hry 
- [ ] Vytvoření vlastního příběhu
- [ ] Návrh mechanik, herní logiky a herních systémů
- [ ] Vytvoření a příprava grafiky (DALL-E, Flux, SD)
- [ ] Návrh UI (ReadingUI, DecidingUI)

### Backend API pro ukládání info o hře - ASP.NET API
- [ ] Návrh databáze 
- [ ] Připojení databáze do aplikace ASP.NET 
- [ ] Návrh struktury API - tedy controllery a REST endpointy 
- [ ] Přihlašování uživatelů do aplikace 
- [ ] Nahrávání, zpracování a získávání souborů (= obrázků)

### Frontend (hráč) - React
- [ ] Založení aplikace 
- [ ] Architektura aplikace 
- [ ] Zpracování dat z formuláře 
- [ ] Ukládání dat do localStorage 
- [ ] Získávání dat z REST API 
- [ ] Manipulace s URL pomocí routeru 
- [ ] Centralizace akcí a dat pomocí Contextu a Reduceru 

### Frontend (krmení API daty) - React?
- [ ] Založení aplikace
- [ ] Přihlašovací formulář pro správce - login / identity
- [ ] Správa databáze (CRUD)
  - [ ] Vytváření nových lokací 
  - [ ] Editace existujících lokací
  - [ ] Mazání lokací
- [ ] Správa obrázků 
  - [ ] Nahrávání obrázků na server (upload)
  - [ ] Mazání obrázků 
  - [ ] Přiřazování obrázků k lokacím
- [ ] Správa herních mechanik (?????)
  - [ ] Vytváření předmětů
  - [ ] Vytváření nepřátel
  - [ ] Nastavení obtížnosti 

**Náměty**
- pasti
- nepřátelé
- logické hádanky a rébusy
- sbírání předmětů
- zámky a klíče
- náhodné předměty
- vylepšování postavy
