using System.Collections.Generic;
using UnityEngine;

public static class RoomData
{
    public readonly static Dictionary<int, Data> roomData;

    static RoomData()
    {
        roomData = new Dictionary<int, Data>()
        {
            { 1, new Data(new Vector2(-7.5f, 0f), 0, "YOU DON'T KNOW WHAT YOU'RE GETTING INTO.") },
            { 2, new Data(new Vector2(-7.5f, 0f), 1, "DON'T EVEN BOTHER TRYING.") },
            { 3, new Data(new Vector2(0f, 0f), 1, "I CAN ALMOST GUARANTEE YOU WILL FALL.") },
            { 4, new Data(new Vector2(0f, 5.5f), 3, "THAT ONE WAS EASY.") },
            { 5, new Data(new Vector2(-8f, 4.5f), new Vector2(7.5f, 4.5f), new Vector2(-8.5f, 2.5f), 0, "THIS ONE IS EASIER THAN YOUR MOTHER." ) },
            { 6, new Data(new Vector2(-8f, 4f), new Vector2(7f, 0f), 4, "DON'T GET DIZZY NOW." ) },
            { 7, new Data(new Vector2(-7.5f, 0f), 4, "HOW FAST CAN YOU GO?" ) },
            { 8, new Data(new Vector2(-4.5f, 3.5f), 3, "DON'T GET CONFUZED, NOW." ) },
            { 9, new Data(new Vector2(-8f, 4f), new Vector2(0f, -2f), 1, "HOW GOOD ARE YOUR REFLEXES?" ) },
            { 10, new Data(new Vector2(-1.5f, 4f), 0, "HARDER THAN IT LOOKS." ) },
            { 11, new Data(new Vector2(7f, 1f), 2, "JUST GIVE UP... IT KEEPS GETTING HARDER." ) },
            { 12, new Data(new Vector2(7f, -2f), new Vector2(-7f, 2f), 1, "I HOPE YOU'RE NOT IN A HURRY." ) },
            { 13, new Data(new Vector2(0f, -4f), 0, "THIS IS WAY TOO EASY.\nSERIOUSLY. NOT HARD." ) },
            { 14, new Data(new Vector2(-7.5f, -1.5f), 0, "IT STARTS TO GET REAL TRICKY HERE." ) },
            { 15, new Data(new Vector2(-8.5f, 4f), 0, "THERE'S AN EASY WAY AND A HARD WAY." ) },
            { 16, new Data(new Vector2(-9f, 0f), 4, "GIVE UP, THIS ONE ISN'T EVEN HARD." ) },
            { 17, new Data(new Vector2(-6.5f, 4.5f), 0, "YOU WON'T BEAT THE GAME." ) },
            { 18, new Data(new Vector2(-6f, 0f), 67, "THIS ONE IS SO HARD YOU'LL NEVER DO IT." ) },
            { 19, new Data(new Vector2(0f, 0f), 0, "NOT SO EASY, IS IT?" ) },
            { 20, new Data(new Vector2(0f, 0f), 0, "IT GETS HARDER NOW." ) },
            { 21, new Data(new Vector2(0f, 0f), 0, "YOU'VE ALREADY LOST." ) },
            { 22, new Data(new Vector2(0f, 0f), 0, "DON'T CHOKE!" ) },
            { 23, new Data(new Vector2(0f, 0f), 0, "AROUND AND AROUND..." ) },
            { 24, new Data(new Vector2(0f, 0f), 0, "THIS ONE ISN'T HARD IF YOU KNOW THE TRICK." ) },
            { 25, new Data(new Vector2(0f, 0f), 0, "YOU'RE PROBABLY GETTING FRUSTRATED." ) },
            { 26, new Data(new Vector2(0f, 0f), 0, "SHOULDN'T EVEN TAKE MORE THAN 2 DEATHS..." ) },
            { 27, new Data(new Vector2(0f, 0f), 0, "NOT HARD AT ALL." ) },
            { 28, new Data(new Vector2(0f, 0f), 0, "BABY WANT HIS BOTTLE?" ) },
            { 29, new Data(new Vector2(0f, 0f), 0, "MIGHT BE TRICKY." ) },
            { 30, new Data(new Vector2(0f, 0f), 0, "IMPOSSIBLE." ) }
        };
    }
}