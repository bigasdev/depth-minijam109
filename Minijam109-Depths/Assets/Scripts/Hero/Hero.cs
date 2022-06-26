using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using BigasMath;
using BigasTools;

public class Hero : MonoBehaviour
{
    private static Hero instance;
    public static Hero Instance{
        get{
            if(instance == null)instance = FindObjectOfType<Hero>();
            return instance;
        }
    }
    [SerializeField] float moveSpeed, jumpForceMultiplier, jumpBoostVanish = .2f;
    [SerializeField] float boostMax = 250, boostMultiplier = .03f, boostMin = 75;
    [SerializeField] BoxCollider2D colliderBox;
    [SerializeField] Rigidbody2D rigidbody2D;
    public SpriteSquash spriteSquash;
    [SerializeField] GameObject spriteRenderer;
    public float currentJumpForce, originalBoostVanish;
    bool triedToJump;
    public bool canCharge;
    public bool isFalling{
        get{
            if(colliderBox.enabled)
            return false;
            else return true;
        }
    }
    GameObject deathPlatform;
    Vector2 originalPos;
    private void Start() {
        originalPos = this.transform.position;
        originalBoostVanish = jumpBoostVanish;
        deathPlatform = Resources.Load<GameObject>("Prefabs/DeathPlatform");
    }
    private void OnCollisionEnter2D(Collision2D other) {
        AudioController.Instance.PlaySound("falling");
        spriteSquash.Squash(.65f, 1);
    }
    private void Update() {
        if(StateController.Instance.currentState != States.GAME_UPDATE)return;
        OnHud();
        if(GameInputManager.GetKeyPress("Dash")){
            spriteSquash.Squash(.65f, 1);
        }
        if(!GameInputManager.GetKeyDown("Interaction")){
            OnJump();
        }
        if(GameInputManager.GetAxisPress("Horizontal") == 1){
            AudioController.Instance.PlaySound("turning");
            spriteSquash.Squash(.65f, 1.2f);
            spriteRenderer.transform.localScale = new Vector2(1,1);
        }
        if(GameInputManager.GetAxisPress("Horizontal") == -1){
            AudioController.Instance.PlaySound("turning");
            spriteSquash.Squash(.65f, 1.2f);
            spriteRenderer.transform.localScale = new Vector2(-1,1);
        }
        if(GameInputManager.GetKeyPress("Interaction") && !canCharge){
            CameraManager.Instance.boostingShake(200f);
            TalkingHudManager.Instance.InitializeHud(this.transform, "I need to get in the tube to be able to jump");
        }
    }
    private void FixedUpdate() {
        if(StateController.Instance.currentState != States.GAME_UPDATE)return;
        var movement = new Vector2(GameInputManager.GetAxisDown("Horizontal") * moveSpeed, 0);
        OnMove(movement);
        if(canCharge)OnBoost();
        OnFall();
    }
    void OnHud(){
        Gamehud.Instance.SetBoostText((int)currentJumpForce);
    }
    void OnBoost(){
        if(isFalling)return;
        if(currentJumpForce <= boostMax){
            if(GameInputManager.GetKeyDown("Interaction")){
                //spriteSquash.Squash(1-(1/currentJumpForce), 1);
                CameraManager.Instance.boostingShake(currentJumpForce);
                currentJumpForce += 1 * jumpForceMultiplier;
                triedToJump = true;
            }
            return;
        }
        currentJumpForce = 0;    
    }
    void OnJump(){
        if(currentJumpForce <= boostMin || isFalling)return;
        rigidbody2D.velocity = new Vector2(0, -(50*GetJumpAmt()));
        AudioController.Instance.PlaySound("tubeThrow");
        spriteSquash.Squash(.65f, 1);
        colliderBox.enabled = false;
        CameraManager.Instance.defaultShake();
        CameraManager.Instance.offset = new Vector2(0, 0);
        triedToJump = false;
    }
    void OnFall(){
        if(!isFalling)return;
        jumpBoostVanish += boostMultiplier;
        currentJumpForce -= 1 * jumpBoostVanish;
        Gamehud.Instance.SetScoreText(GetScore());
        if(currentJumpForce <= 0){
            CameraManager.Instance.offset = new Vector2(0, 3);
            currentJumpForce = 0;
            colliderBox.enabled = true;
            var d = Instantiate(deathPlatform);
            d.transform.position = new Vector2(0, this.transform.position.y - 1.5f);
            jumpBoostVanish = originalBoostVanish;
            Engine.Instance.SaveScore(GetScore());
            Gamehud.Instance.EndGame();
            StateController.Instance.ChangeState(States.GAME_IDLE);
        }
    }
    void OnMove(Vector3 movement){
        this.transform.position += movement;
    }
    public int GetScore(){
        var d = originalPos.y - this.transform.position.y;
        return (int)d;
    }
    public void SetChargeState(bool state){
        if(state == canCharge || isFalling)return;
        canCharge = state;
        if(state!=true){
            TalkingHudManager.Instance.InitializeHud(this.transform, "Leaving the tube!");
        }
        if(state==true){
            AudioController.Instance.PlaySound("enteringTube");
        }
    }
    public float GetJumpAmt(){
        return BMathPercentage.GetPercentageFromFloat(currentJumpForce, boostMax) * .01f;
    }
    public void ChangeVelocity(float amt){
        rigidbody2D.velocity = new Vector2(rigidbody2D.velocity.x, rigidbody2D.velocity.y * amt);
    }
    public void ChangeBoost(int amt){
        currentJumpForce += amt;
    }
}
