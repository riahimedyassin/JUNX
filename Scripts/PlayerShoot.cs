using UnityEngine;
using Mirror;

public class PlayerShoot : NetworkBehaviour 
{
    public PlayerWeapon weapon;
    [SerializeField]
    private Camera cam;
    [SerializeField]
    private LayerMask mask;

    void Start()
    {
        if(cam==null) {
            Debug.Log("Pas de cemera renseignée sur le systeme de tir");
            this.enabled=false;
        }
    }
    private void Update() {
        if(PauseMenu.isOn) {
            return;
        }
        if(Input.GetButtonDown("Fire1")) {
            Shoot();
        }
    }
    [Client]
    private void Shoot() {
        RaycastHit hit ;
        
        if (Physics.Raycast(cam.transform.position,cam.transform.forward,out hit,weapon.range,mask) ) {
            if(hit.collider.tag=="Player") {
                CmdPlayerShot(hit.collider.name,weapon.damage);
            }
        }
    }

    [Command]
    private void CmdPlayerShot(string playerId,float damage) {
        Debug.Log(playerId +"a ete touché");
        Player player= GameManager.GetPlayer(playerId);
        player.RpcTakeDamage(damage);
    }

    
   
}
