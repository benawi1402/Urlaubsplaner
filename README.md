# Urlaubsplaner

Eine einfache WPF Anwendung zur Beantragung und Genehmigung von Urlaub.

## Allgemeine Hinweise zur Applikation

Die Applikation weicht von den Anforderungen leicht ab. Das Antragsformular wurde in einen Dialog verschoben, der nach dem Login von überall erreichbar ist.
In dem ersten Tab können die eigenen Anträge eingesehen werden. Sollte der Benutzer aber auch eine TEAMLEADER Rolle haben, darf er auch die Anträge seines Teams sehen, nicht aber seine eigenen.
Der Einzige, der TEAMLEADER Anträge genehmigen kann, ist der ADMIN.

Angelegte Anträge werden als XML in der Datei "storage.xml" abgespeichert.

## Benutzerdaten

Hier sind die angelegten Benutzer, ihre Rollen und Passwörter aufgelistet.
Alle Benutzer haben das Passwort "test123" hinterlegt.

|ID|Benutzername|Voller Name|Rollen|
|--|------------|-----------|------|
|1 |adradm00    |Adrian Admin|ADMIN|
|2 |maxmus00    |Max Muster|USER|
|3 |annmus00    |Annika Muster|USER|
|4 |larlea00	|Lara Leader|TEAMLEADER|
|5 |manmer00    |Manfred Meerfeld|USER|
|6 |gudgan00    |Gudrun Gans|USER|
|7 |leolea00    |Leon Leader|TEAMLEADER|