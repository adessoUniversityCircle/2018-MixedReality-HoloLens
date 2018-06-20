/***
* HILFREICHE LINKS
***/
https://github.com/Microsoft/MixedRealityToolkit-Unity
	https://github.com/Microsoft/MixedRealityToolkit-Unity/blob/master/Assets/HoloToolkit/Input/README.md
	https://github.com/Microsoft/MixedRealityToolkit-Unity/blob/master/Assets/HoloToolkit/SpatialMapping/README.md
https://docs.microsoft.com/en-us/windows/mixed-reality/gestures
https://docs.microsoft.com/en-us/windows/mixed-reality/development


/***
* SCENE HINZUFÜGEN & VIA BUTTON LADEN LASSEN
***/
1. Scenes-Ordner -> Rechtsklick -> Create -> Scene -> Name der Scene eingeben
2. Scene per Drag/Drop in Hierarchy ziehen
3. MainCamera und Directional Light aus neuer Scene löschen
4. Menuleiste -> File -> Build Settings -> Add Open Scenes -> Dialog schließen (x) 
5. Hierarchy -> UI -> SceneSelectionMenu -> Scenes -> Content -> Existierenden Button kopieren und in Content hinzufügen
	-> umbennen in Btn<SceneName> -> in "Compound Button Text"-Komponente des Buttons beliebigen Namen eingeben
6. Hierarchy -> UI -> SceneSelectionMenu -> Scenes -> Content -> "ObjectCollection"-Komponente Button "Update Collection" klicken (der
	neu hinzugefügt Button sollte dann richtig eingerückt sein)
7. [Test] Play-Mode -> Button der Scene anklicken -> Scene sollte additive hinzugefügt werden

/***
* SCENE BEARBEITEN
***/
1. (Wenn nicht schon getan) Scene in Hierarchy ziehen
2. GameObjects, Script, etc. hinzufügen
3. [Test] Scene aus Hierarchy entfernen (Hierarchy -> Rechtsklick auf Scene -> Remove Scene)

/***
* ANPASSEN WAS BEI WIEDERHOLTEM KLICK AUF LADE BUTTON GESCHEHEN SOLL
***/
1. SceneSelectionMenu -> SceneMenuReceiver-Komponente öffnen (Doppelklick auf Script-Feld)
	-> Script öffnet sich in Visual Studio -> HandleSceneSelection -> Ähnlich wie für
	AdessoLogo Implementation hinzufügen