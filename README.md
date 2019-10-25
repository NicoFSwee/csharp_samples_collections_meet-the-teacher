* Aufgetreteme Probleme:

Problem beim Einfügen der Lehrer, da zu erst alle eingefügt wurden, die Details haben und anschließend die restlichen.

Gelöst:
2 foreach-Schleifen vergleichen den Namen der derzeitigen Zeile mit dem derzeitigen Key des Dictionarys, sind sie gleich wird ein neuer Lehrer angelegt. Anschließend wird erneut mit einer foreach Schleife, welche über die ganze liste itteriert, überprüft ob der jeweilige Name schon in der Lehrer-Liste eingespeichert ist. Wenn nicht, dann wird ein neuer Lehrer angelegt.
