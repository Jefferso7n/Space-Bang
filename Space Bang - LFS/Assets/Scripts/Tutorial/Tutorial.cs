using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
//Sim, isso tá horrível

public class Tutorial : MonoBehaviour
{
    [SerializeField] Transform player;
    [SerializeField] float circleRaycastRadius = 7f;
    [SerializeField] LayerMask enemyMask;

    public bool hasFoundEnemy, hasShooted, hasReachedMaxMoveSpeed, hasShootedWithAllWeapons, isEndingTutorial;
    public bool healthTuto, timerTuto, killsTuto, endTuto;

    [Header("Weapons")]
    [SerializeField] GameObject shotgun;
    [SerializeField] GameObject pistol;
    [SerializeField] GameObject machineGun;

    [Header("Movement")]
    [SerializeField] PlayerController playerController;
    [SerializeField] PlayerSpeed playerSpeed;
    public float timerOnMovement = 0f;
    [SerializeField] float maxTimerOnMovement = 5f;

    [Header("Change Weapon")]
    [SerializeField] WeaponSwitching weaponSwitching;
    [SerializeField] Shooting pistolShooting;
    public bool hasShootedWithPistol;
    [SerializeField] Shooting machineGunShooting;
    public bool hasShootedWithMachineGun;

    [Header("UI")]
    [SerializeField] GameObject panelGO;
    [SerializeField] GameObject imageGO;
    [SerializeField] TMP_Text textTutorial;
    [SerializeField] Image imageTutorial;
    [Space]
    [SerializeField] Timer timer;

    [Header("Sprites")]
    [SerializeField] Sprite leftClickSprite;
    [SerializeField] Sprite wsadSprite;
    [SerializeField] Sprite scrollSprite;

    [Header("Canvas")]
    [SerializeField] Canvas healthCanvas;
    [SerializeField] Canvas timerCanvas;
    [SerializeField] Canvas killsCanvas;
    [SerializeField] Canvas weaponCanvas;

    [Header("EnemyPooling")]
    [SerializeField] GameObject enemyPooling;

    private void Update()
    {
        if (!isEndingTutorial)
        {
            RaycastCircle();
            if (!hasFoundEnemy) return;

            RaycastBullet();
            SetTutorial("CLIQUE COM O <color=green>BOTÃO ESQUERDO</color> DO MOUSE EM DIREÇÃO AO <color=green>INIMIGO</color>.", leftClickSprite);
            if (!hasShooted) return;

            EnableMovement();
            SetTutorial("SEGURE <color=green>WSAD</color> PARA <color=yellow>VOAR</color> NA DIREÇÃO DESEJADA.", wsadSprite);
            if (!hasReachedMaxMoveSpeed) return;

            ShootWithAllWeapons();
            SetTutorial("<color=green>GIRE</color> O <color=green>SCROLL</color> DO MOUSE PARA <color=green>TROCAR DE ARMA</color> E <color=red>DÊ UM TIRO COM CADA ARMA</color>.\nARMAS DISPONIVEIS: <color=green>ESCOPETA</color>, <color=red>PISTOLA</color>, <color=yellow>METRALHADORA</color>.", scrollSprite);
            if (!hasShootedWithAllWeapons) return;
            isEndingTutorial = true;
        }
        else
        {
            imageGO.SetActive(false);
            MaisAlgumasCoisinhas();
        }

    }

    void PauseGame()
    {
        Time.timeScale = 0f;
        ShowUI();
    }

    void UnpauseGame()
    {
        Time.timeScale = 1f;
        //        HideUI();
    }

    void RaycastCircle()
    {
        RaycastHit2D circleCast = Physics2D.CircleCast(player.position, circleRaycastRadius, Camera.main.transform.forward, circleRaycastRadius, enemyMask);
        if (circleCast.collider == null) return;
        if (!circleCast.collider.CompareTag("Enemy")) return;
        if (hasFoundEnemy) return;

        hasFoundEnemy = true;
        PauseGame();
    }

    void RaycastBullet()
    {
        if (Input.GetMouseButtonDown(0))
        {
            var mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            RaycastHit2D bulletCast = Physics2D.Raycast(player.position, mousePos - player.position, 50 * circleRaycastRadius, enemyMask);

            if (bulletCast.collider == null) return;
            if (!bulletCast.collider.CompareTag("Enemy")) return;
            if (hasShooted) return;
            shotgun.GetComponent<Shotgun>().canShoot = true;
//            shotgun.GetComponent<Shotgun>().Shoot();

            hasShooted = true;
            UnpauseGame();
        }
    }

    void EnableMovement()
    {
        playerController.canMove = true;

        if (hasReachedMaxMoveSpeed) return;
        if (playerSpeed.GetSpeed() >= playerSpeed.GetMaxSpeed())
        {
            timerOnMovement += Time.fixedDeltaTime;

            if (timerOnMovement >= maxTimerOnMovement)
            {
                hasReachedMaxMoveSpeed = true;
                PauseGame();
            }
        }
        if (playerController.movement == Vector2.zero)
        {
            timerOnMovement = 0f;
        }
    }

    void ShootWithAllWeapons()
    {
        weaponSwitching.canSwitchWeapon = true;
        weaponCanvas.gameObject.SetActive(true);

        if (hasShootedWithAllWeapons) return;

        if (Input.GetAxis("Mouse ScrollWheel") > 0f)
        {
            UnpauseGame();
        }

        if (weaponSwitching.GetSelectedWeapon().name == "Pistol")
        {
            pistolShooting.canShoot = true;

            if (Input.GetMouseButton(0))
            {
                hasShootedWithPistol = true;
            }
        }

        if (weaponSwitching.GetSelectedWeapon().name == "Machine Gun")
        {
            machineGunShooting.canShoot = true;

            if (Input.GetMouseButton(0))
            {
                hasShootedWithMachineGun = true;
            }
        }

        if (!hasShootedWithPistol || !hasShootedWithMachineGun) return;

        hasShootedWithAllWeapons = true;
    }

    #region UI
    void SetTutorial(string tutorialText, Sprite sprite)
    {
        textTutorial.text = tutorialText;
        imageTutorial.sprite = sprite;
    }

    void ShowUI()
    {
        imageGO.SetActive(true);
        panelGO.SetActive(true);
    }

    void HideUI()
    {
        imageGO.SetActive(false);
        panelGO.SetActive(false);
    }
    #endregion

    #region Timer And Kills
    void MaisAlgumasCoisinhas()
    {
        if (!healthTuto)
            SetTutorial("VOU TE APRESENTAR UM POUCO DA <color=green>INTERFACE</color> DO JOGO.\nMAS COMO DIRIA <color=yellow>JACK, O ESTRIPADOR</color>: VAMOS POR <color=red>PARTES!</color>", leftClickSprite);
        Invoke("TutorialHealth", 8f);
    }

    void TutorialHealth()
    {
        healthTuto = true;
        healthCanvas.gameObject.SetActive(true);
        if (!timerTuto)
            SetTutorial("QUANDO UM <color=red>INIMIGO</color> <color=yellow>ENCOSTAR</color> EM <color=green>VOCÊ</color>, SUA <color=green>VIDA</color> <color=red>DECAIRÁ</color>.\nQUANDO A <color=red>BARRA DE VIDA</color> <color=yellow>ESVAZIAR</color> É <color=red>FIM DE JOGO</color>.", leftClickSprite);
        Invoke("TutorialTimer", 11f);
    }

    void TutorialTimer()
    {
        timerTuto = true;
        timerCanvas.gameObject.SetActive(true);
        timer.UpdateTimerDisplay(60f);
        if (!killsTuto)
            SetTutorial("O <color=yellow>TEMPORIZADOR</color> EXIBE <color=green>QUANTO TEMPO</color> TE RESTA <color=yellow>ATÉ</color> FICAR SEM OXIGÊNIO E <color=red>MORRER</color>.", leftClickSprite);
        Invoke("TutorialKills", 7f);
    }

    void TutorialKills()
    {
        killsTuto = true;
        killsCanvas.gameObject.SetActive(true);
        if (!endTuto)
            SetTutorial("A BOA NOTÍCIA É QUE:\nAO <color=yellow>ELIMINAR</color> <color=green>10</color> <color=red>INIMIGOS</color> O SEU <color=yellow>TEMPORIZADOR</color> É <color=green>ACRESCENTADO</color> EM <color=yellow>20 SEGUNDOS</color>.", leftClickSprite);
        Invoke("EndTutorial", 10f);
    }

    void EndTutorial()
    {
        endTuto = true;
        enemyPooling.SetActive(true);
        weaponCanvas.gameObject.SetActive(true);
        timer.gameObject.SetActive(true);
        timer.isCounting = true;

        SetTutorial("<color=yellow>QUE COMECEM OS JOGOS!</color>", leftClickSprite);
        Destroy(gameObject, 5f);
    }

    #endregion

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(player.position, circleRaycastRadius);
    }

}
