using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public@static class TitleStatic
{
    private static int _deckNumber;

    public static void SetDeckNumber(int deckNumber) {  _deckNumber = deckNumber; }
    public static int GetDeckNumber() {  return _deckNumber; }
}
