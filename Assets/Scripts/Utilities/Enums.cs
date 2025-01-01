
using System;

[Flags]
public enum RoomType
{
    Guidance = 1, MiniorEnemy = 2, EliteEnemy = 4, Shop = 8, Treasure = 16, RestRoom = 32, Boss = 64
}

public enum RoomState
{
    Locked, Visited, Attainable
}

public enum CardType
{
    Attack, Defense, Abilities
}

public enum Rarity
{
    Normal, Superior, Elite, Epic, Legendary, Mythical
}

[Flags]
public enum MultipleRarity
{
    Normal = 1, Superior = 2, Elite = 4, Epic = 8, Legendary = 16, Mythical = 32
}

public enum CardTagType
{
    Metal, //保留在手上，不会每回合进入弃牌堆 Keep in hand, will not enter the discard deck each round
    Wood, //
    Water,//
    Fire,//
    Earth,//
    Air,//每场战斗结束后将会消失 Disappear after each battle
    Lighting,//使用后进入弃牌堆后将会消失   Disappear after entering the discard deck after use
    Darkness//进入弃牌堆后将会消失  Disappear after entering the discard deck
}

public enum EffectTargetType
{
    Self, Enemy, AllEnemies
}

public enum EffectDurationType
{
    Once, Sustainable, Permanent
}
