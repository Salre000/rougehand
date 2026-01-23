using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public@static class TitlStatic
{
    private static int _deckNumber=-1;

    public static void SetDeckNumber(int deckNumber) {  _deckNumber = deckNumber; }
    public static int GetDeckNumber() {  return _deckNumber; }
}
