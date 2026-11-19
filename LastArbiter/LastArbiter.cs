using BepInEx;
using BepInEx.Logging;
using HarmonyLib;
using HutongGames.PlayMaker;
using HutongGames.PlayMaker.Actions;
using System;
using System.Linq;
using UnityEngine;

[BepInPlugin("com.slimybonesjones.lastarbiter", "Last Arbiter", "1.0.0")]

public class LastArbiter : BaseUnityPlugin
{
    internal static ManualLogSource Log;
    internal static HealthManager healthManager;
    private void Awake()
    {
        Log = Logger;
        Log.LogInfo("Plugin loaded and initialised");

        Harmony.CreateAndPatchAll(typeof(LastArbiter), null);
    }

    [HarmonyPostfix]
    [HarmonyPatch(typeof(PlayMakerFSM), "Awake")]

    private static void Postfix(PlayMakerFSM __instance)
    {
        var owner = __instance?.gameObject;

        if (owner == null) return;
        if (!owner.name.StartsWith("Last Judge", StringComparison.OrdinalIgnoreCase)) return;
        if (!string.Equals(__instance.FsmName, "Control", StringComparison.OrdinalIgnoreCase)) return;

        //Idle

        var stateIdle = __instance.Fsm.GetState("Idle");

        if (stateIdle == null)
        {
            Log.LogWarning("No state named Idle");
            return;
        }

        var waitStateIdle = stateIdle.Actions.OfType<Wait>().FirstOrDefault(a => a.time.Name == "Idle Time");
        waitStateIdle.time = 0f;

        //Init

        var stateInit = __instance.Fsm.GetState("Init");

        if (stateInit == null)
        {
            Log.LogWarning("No state named Init");
            return;
        }

        var mibfP2StateInit = stateInit.Actions.OfType<MultiplyIntByFloat>().FirstOrDefault(a => a.storeResult.Name == "HP P2");
        mibfP2StateInit.multiplyFloat = 0.85f;

        var mibfP3StateInit = stateInit.Actions.OfType<MultiplyIntByFloat>().FirstOrDefault(a => a.storeResult.Name == "HP P3");
        mibfP3StateInit.multiplyFloat = 0.25f;

        //Spinning

        var stateSpinning = __instance.Fsm.GetState("Spinning");

        if (stateSpinning == null)
        {
            Log.LogWarning("No state named Spinning");
            return;
        }

        var waitStateSpinning = stateSpinning.Actions.OfType<Wait>().FirstOrDefault();
        waitStateSpinning.time = 0.5f;


        var stateRerage = __instance.Fsm.GetState("Rerage?");

        if (stateRerage == null)
        {
            Log.LogWarning("No state named Rerage?");
            return;
        }

        var intCompareStateRerage = stateRerage.Actions.OfType<IntCompare>().FirstOrDefault();
        intCompareStateRerage.integer2 = -16;


        var stateRageEnd = __instance.Fsm.GetState("Rage End");

        if (stateRageEnd == null)
        {
            Log.LogWarning("No state named Rage End");
            return;
        }

        var setFloatValueStateRageEnd = stateRageEnd.Actions.OfType<SetFloatValue>().FirstOrDefault();
        setFloatValueStateRageEnd.floatValue = 0.2f;


        var stateFlameSpinAnticL = __instance.Fsm.GetState("Flame Spin Antic L");

        if (stateFlameSpinAnticL == null)
        {
            Log.LogWarning("No state named Flame Spin Antic L");
            return;
        }

        var waitStateFlameSpinAnticL = stateFlameSpinAnticL.Actions.OfType<Wait>().FirstOrDefault();
        waitStateFlameSpinAnticL.time = 0f;


        var stateFlameDir = __instance.Fsm.GetState("Flame Dir");

        if (stateFlameDir == null)
        {
            Log.LogWarning("No state named Flame Dir");
            return;
        }

        var randomFloatStateFlameDir = stateFlameDir.Actions.OfType<RandomFloat>().FirstOrDefault(a => a.storeResult.Name == "Speed R");
        randomFloatStateFlameDir.min = 7.5f;
        randomFloatStateFlameDir.max = 15f;


        var stateLFlameR = __instance.Fsm.GetState("L Flame R");

        if (stateLFlameR == null)
        {
            Log.LogWarning("No state named L Flame R");
            return;
        }

        var waitStateLFlameR = stateLFlameR.Actions.OfType<Wait>().FirstOrDefault(a => a.time.Value == 1.7f);
        waitStateLFlameR.time = 0.7f;

        var waitBoolStateLFlameR = stateLFlameR.Actions.OfType<WaitBool>().FirstOrDefault();
        waitBoolStateLFlameR.time = 0.5f;

        var setrdStateLFlameR = stateLFlameR.Actions.OfType<SendEventToRegisterDelay>().FirstOrDefault();
        setrdStateLFlameR.delay = 0.5f;

        var redWaitStateLFlameR = stateLFlameR.Actions.OfType<Wait>().FirstOrDefault(a => a.time.Value == 1.25f);
        redWaitStateLFlameR.time = 0.5f;


        var stateRFlameL = __instance.Fsm.GetState("R Flame L");

        if (stateRFlameL == null)
        {
            Log.LogWarning("No state named R Flame L");
            return;
        }

        var waitStateRFlameL = stateRFlameL.Actions.OfType<Wait>().FirstOrDefault(a => a.time.Value == 1.7f);
        waitStateRFlameL.time = 0.7f;

        var waitBoolStateRFlameL = stateRFlameL.Actions.OfType<WaitBool>().FirstOrDefault();
        waitBoolStateRFlameL.time = 0.5f;

        var setrdStateRFlameL = stateRFlameL.Actions.OfType<SendEventToRegisterDelay>().FirstOrDefault();
        setrdStateLFlameR.delay = 0.5f;

        var redWaitStateRFlameL = stateRFlameL.Actions.OfType<Wait>().FirstOrDefault(a => a.time.Value == 1.25f);
        redWaitStateLFlameR.time = 0.5f;

        //Charge

        var stateCharge = __instance.Fsm.GetState("Charge");

        if (stateCharge == null)
        {
            Log.LogWarning("No state named Charge");
            return;
        }

        var waitStateCharge = stateCharge.Actions.OfType<Wait>().FirstOrDefault();
        waitStateCharge.time = 1f;


        var stateChargeFlame = __instance.Fsm.GetState("Charge Flame");

        if (stateChargeFlame == null)
        {
            Log.LogWarning("No state named Charge Flame");
            return;
        }

        var waitStateChargeFlame = stateChargeFlame.Actions.OfType<Wait>().FirstOrDefault();
        waitStateChargeFlame.time = 0f;


        var stateChargeAntic2 = __instance.Fsm.GetState("Charge Antic 2");

        if (stateChargeAntic2 == null)
        {
            Log.LogWarning("No state named Charge Antic 2");
            return;
        }

        var waitStateChargeAntic2 = stateChargeAntic2.Actions.OfType<Wait>().FirstOrDefault();
        waitStateChargeAntic2.time = 0.2f;


        var stateChargeStart = __instance.Fsm.GetState("Charge Start");

        if (stateChargeStart == null)
        {
            Log.LogWarning("No state named Charge Start");
            return;
        }

        var svbsStateChargeStart = stateChargeStart.Actions.OfType<SetVelocityByScale>().FirstOrDefault();
        svbsStateChargeStart.speed = -22f;

        //Stomp

        var stateStompFlameAntic = __instance.Fsm.GetState("Stomp Flame Antic");

        if (stateStompFlameAntic == null)
        {
            Log.LogWarning("No state named Stomp Flame Antic");
            return;
        }

        var waitStateStompFlameAntic = stateStompFlameAntic.Actions.OfType<Wait>().FirstOrDefault();
        waitStateStompFlameAntic.time = 0f;


        var stateStompFlames = __instance.Fsm.GetState("Stomp Flames");

        if (stateStompFlames == null)
        {
            Log.LogWarning("No state named Stomp Flames");
            return;
        }

        var waitStateStompFlames = stateStompFlames.Actions.OfType<Wait>().FirstOrDefault();
        waitStateStompFlames.time = 0.7f;

        var spto2dStateStompFlameAntic1 = stateStompFlameAntic.Actions.OfType<SetPositionToObject2D>().FirstOrDefault(a => a.xOffset.Value == -2f);
        var flameWaveL = spto2dStateStompFlameAntic1.gameObject.GameObject;

        var spto2dStateStompFlameAntic2 = stateStompFlameAntic.Actions.OfType<SetPositionToObject2D>().FirstOrDefault(a => a.xOffset.Value == 2f);
        var flameWaveR = spto2dStateStompFlameAntic2.gameObject.GameObject;

        var sofgpStateStompFlames = stateStompFlames.Actions.OfType<SpawnObjectFromGlobalPool>().FirstOrDefault();
        var stompFlamesSpawnPoint = sofgpStateStompFlames.spawnPoint;

        var setScaleStateStompFlames1 = stateStompFlames.Actions.OfType<SetScale>().FirstOrDefault(a => a.x.Value == 1f);
        var setScaleGameObject1 = setScaleStateStompFlames1.gameObject;

        var setScaleStateStompFlames2 = stateStompFlames.Actions.OfType<SetScale>().FirstOrDefault(a => a.x.Value == -1f);
        var setScaleGameObject2 = setScaleStateStompFlames2.gameObject;

        var sofgpotAction1 = new SpawnObjectFromGlobalPoolOverTime
        {
            gameObject = flameWaveL,
            spawnPoint = stompFlamesSpawnPoint,
            position = new FsmVector3 { Value = new Vector3(-4f, 0f, 0f) },
            rotation = new FsmVector3 { Value = new Vector3( 0f, 0f, 0f) },
            frequency = 0.2f
        };

        var setScaleAction1 = new SetScale
        {
            gameObject = setScaleGameObject1,
            vector = new FsmVector3 { Value = new Vector3(0f, 0f, 0f) },
            x = 1f,
            y = 0,
            z = 0
        };

        var sofgpotAction2 = new SpawnObjectFromGlobalPoolOverTime
        {
            gameObject = flameWaveR,
            spawnPoint = stompFlamesSpawnPoint,
            position = new FsmVector3 { Value = new Vector3(4f, 0f, 0f) },
            rotation = new FsmVector3 { Value = new Vector3(0f, 0f, 0f) },
            frequency = 0.2f
        };

        var setScaleAction2 = new SetScale
        {
            gameObject = setScaleGameObject2,
            vector = new FsmVector3 { Value = new Vector3(0f, 0f, 0f) },
            x = -1f,
            y = 0,
            z = 0
        };

        stateStompFlames.Actions = stateStompFlames.Actions.Append(sofgpotAction1).ToArray();
        stateStompFlames.Actions = stateStompFlames.Actions.Append(setScaleAction1).ToArray();
        stateStompFlames.Actions = stateStompFlames.Actions.Append(sofgpotAction2).ToArray();
        stateStompFlames.Actions = stateStompFlames.Actions.Append(setScaleAction2).ToArray();


        var stateStompRegular = __instance.Fsm.GetState("Stomp Regular");

        if (stateStompRegular == null)
        {
            Log.LogWarning("No state named Stomp Regular");
            return;
        }

        var waitStateStompRegular = stateStompRegular.Actions.OfType<Wait>().FirstOrDefault();
        waitStateStompRegular.time = 0.1f;


        var stateStompRiseAndAntic = __instance.Fsm.GetState("Stomp Rise and Antic");

        if (stateStompRiseAndAntic == null)
        {
            Log.LogWarning("No state named Stomp Rise and Antic");
            return;
        }

        var easeFloatXStateStompRiseAndAntic = stateStompRiseAndAntic.Actions.OfType<EaseFloat>().FirstOrDefault(a => a.floatVariable.Name == "Tween X");
        easeFloatXStateStompRiseAndAntic.time = 0.2f;

        var easeFloatYStateStompRiseAndAntic = stateStompRiseAndAntic.Actions.OfType<EaseFloat>().FirstOrDefault(a => a.floatVariable.Name == "Tween Y");
        easeFloatYStateStompRiseAndAntic.time = 0.1f;

        var agodStateStompRiseAndAntic = stateStompRiseAndAntic.Actions.OfType<ActivateGameObjectDelay>().FirstOrDefault(a => a.activate.Name == "Flamed Up");
        agodStateStompRiseAndAntic.delay = 0.1f;

        //Jump

        var stateJumpRise = __instance.Fsm.GetState("Jump Rise");

        if (stateJumpRise == null)
        {
            Log.LogWarning("No state named Jump Rise");
            return;
        }

        var easeFloatXStateJumpRise = stateJumpRise.Actions.OfType<EaseFloat>().FirstOrDefault(a => a.floatVariable.Name == "Tween X");
        easeFloatXStateJumpRise.time = 0.2f;

        var easeFloatYStateJumpRise = stateJumpRise.Actions.OfType<EaseFloat>().FirstOrDefault(a => a.floatVariable.Name == "Tween Y");
        easeFloatYStateJumpRise.time = 0.15f;


        var stateJumpFall = __instance.Fsm.GetState("Jump Fall");

        if (stateJumpFall == null)
        {
            Log.LogWarning("No state named Jump Fall");
            return;
        }

        var easeFloatXStateJumpFall = stateJumpFall.Actions.OfType<EaseFloat>().FirstOrDefault(a => a.floatVariable.Name == "Tween X");
        easeFloatXStateJumpFall.time = 0.15f;

        var easeFloatYStateJumpFall = stateJumpFall.Actions.OfType<EaseFloat>().FirstOrDefault(a => a.floatVariable.Name == "Tween Y");
        easeFloatYStateJumpFall.time = 0.2f;

        //Dash

        var stateDash = __instance.Fsm.GetState("Dash");

        if (stateDash == null)
        {
            Log.LogWarning("No state named Dash");
            return;
        }

        var svbsStateDash = stateDash.Actions.OfType<SetVelocityByScale>().FirstOrDefault();
        svbsStateDash.speed = -40f;

        //Throw

        var stateClampL = __instance.Fsm.GetState("Clamp L");

        if (stateClampL == null)
        {
            Log.LogWarning("No state named Clamp L");
            return;
        }

        var floatClampStateClampL = stateClampL.Actions.OfType<FloatClamp>().FirstOrDefault(a => a.floatVariable.Name == "X Distance");
        floatClampStateClampL.minValue = -24f;
        floatClampStateClampL.maxValue = -8f;


        var stateClampR = __instance.Fsm.GetState("Clamp R");

        if (stateClampR == null)
        {
            Log.LogWarning("No state named Clamp R");
            return;
        }

        var floatClampStateClampR = stateClampR.Actions.OfType<FloatClamp>().FirstOrDefault(a => a.floatVariable.Name == "X Distance");
        floatClampStateClampR.minValue = 8f;
        floatClampStateClampR.maxValue = 24f;


        var stateThrowRise = __instance.Fsm.GetState("Throw Rise");

        if (stateThrowRise == null)
        {
            Log.LogWarning("No state named Throw Rise");
            return;
        }

        var easeFloatXStateThrowRise = stateThrowRise.Actions.OfType<EaseFloat>().FirstOrDefault(a => a.floatVariable.Name == "Tween X");
        easeFloatXStateThrowRise.time = 0.1f;

        var easeFloatYStateThrowRise = stateThrowRise.Actions.OfType<EaseFloat>().FirstOrDefault(a => a.floatVariable.Name == "Tween Y");
        easeFloatYStateThrowRise.time = 0.1f;

        var waitStateThrowRise = stateThrowRise.Actions.OfType<Wait>().FirstOrDefault();
        waitStateThrowRise.time = 0f;


        var stateThrowFall = __instance.Fsm.GetState("Throw Fall");

        if (stateThrowFall == null)
        {
            Log.LogWarning("No state named Throw Fall");
            return;
        }

        var easeFloatXStateThrowFall = stateThrowFall.Actions.OfType<EaseFloat>().FirstOrDefault(a => a.floatVariable.Name == "Tween X");
        easeFloatXStateThrowFall.time = 0.1f;

        var easeFloatYStateThrowFall = stateThrowFall.Actions.OfType<EaseFloat>().FirstOrDefault(a => a.floatVariable.Name == "Tween Y");
        easeFloatYStateThrowFall.time = 0.1f;


        var stateSlamType = __instance.Fsm.GetState("Slam Type");

        if (stateSlamType == null)
        {
            Log.LogWarning("No state named Slam Type");
            return;
        }

        var cbtfStateSlamType = stateSlamType.Actions.OfType<ConvertBoolToFloat>().FirstOrDefault();
        cbtfStateSlamType.falseValue = 0f;

        //Stun

        var stateStunStart = __instance.Fsm.GetState("Stun Start");

        if (stateStunStart == null)
        {
            Log.LogWarning("No state named Stun Start");
            return;
        }

        var setFloatValueStateStunStart = stateStunStart.Actions.OfType<SetFloatValue>().FirstOrDefault(a => a.floatVariable.Name == "Stun Timer");
        setFloatValueStateStunStart.floatValue = 1f;

        //Close Range

        var stateCloseRange = __instance.Fsm.GetState("Close Range");

        if (stateCloseRange == null)
        {
            Log.LogWarning("No state named Close Range");
            return;
        }

        var sreStateCloseRange = stateCloseRange.Actions.OfType<SendRandomEventV4>().FirstOrDefault();
        sreStateCloseRange.weights = new FsmFloat[3] { 0.4f, 0.4f, 0.2f };
        sreStateCloseRange.eventMax = new FsmInt[3] { 2, 2, 1 };
        sreStateCloseRange.missedMax = new FsmInt[3] { 4, 3, 6 };

        //Far Range

        var stateFarRange = __instance.Fsm.GetState("Far Range");

        if (stateFarRange == null)
        {
            Log.LogWarning("No state named Far Range");
            return;
        }

        var sreStateFarRange = stateFarRange.Actions.OfType<SendRandomEventV4>().FirstOrDefault();
        sreStateFarRange.weights = new FsmFloat[4] { 0.3f, 0.3f, 0.2f, 0.2f };
        sreStateFarRange.eventMax = new FsmInt[4] { 1, 1, 2, 2 };
        sreStateFarRange.missedMax = new FsmInt[4] { 2, 2, 3, 2 };

        //Close Range F

        var stateCloseRangeF = __instance.Fsm.GetState("Close Range F");

        if (stateCloseRangeF == null)
        {
            Log.LogWarning("No state named Close Range F");
            return;
        }

        var sreStateCloseRangeF = stateCloseRangeF.Actions.OfType<SendRandomEventV3>().FirstOrDefault();
        sreStateCloseRangeF.weights = new FsmFloat[5] { 0.3f, 0.25f, 0.15f, 0f, 0.3f };
        sreStateCloseRangeF.eventMax = new FsmInt[5] { 1, 3, 1, 2, 1 };
        sreStateCloseRangeF.missedMax = new FsmInt[5] { 4, 2, 999, 999, 4 };

        //Far Range F

        var stateFarRangeF = __instance.Fsm.GetState("Far Range F");

        if (stateFarRangeF == null)
        {
            Log.LogWarning("No state named Far Range F");
            return;
        }

        var sreStateFarRangeF = stateFarRangeF.Actions.OfType<SendRandomEventV3>().FirstOrDefault();
        sreStateFarRangeF.weights = new FsmFloat[5] { 0.2f, 0.2f, 0.2f, 0.1f, 0.3f };
        sreStateFarRangeF.eventMax = new FsmInt[5] { 1, 2, 1, 1, 3 };
        sreStateFarRangeF.missedMax = new FsmInt[5] { 2, 3, 999, 4, 2 };

        //Health

        healthManager = __instance.gameObject.GetComponent<HealthManager>();
        healthManager.hp = 1440;
    }
}