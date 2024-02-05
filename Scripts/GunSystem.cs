    using UnityEngine;
    using TMPro;

    public class GunSystem : MonoBehaviour
    {
        public int damage;
        public float timeBetweenShooting, spread, range, timeBetweenShots;
        public int bulletsPerTap;
        public int magazineSize = 30; 

        public bool allowButtonHold;

        int bulletsLeft, bulletsShot;

        bool shooting, readyToShoot, reloading;

        public Transform gunBarrel;
        public GameObject bulletPrefab;
        public float bulletSpeed = 100f;
        public float smoothingFactor = 0.2f;
        public float shootTimer = 0;
        public float shootInterval = 0.2f;

        public TextMeshProUGUI text;


        private RaycastHit rayHit;

        void Awake()
        {
            bulletsLeft = magazineSize;
            readyToShoot = true;
        }

        void Start()
        {

        }

        void Update()
        {
            MyInput();

            if (bulletsLeft == magazineSize)
            {
                text.text = magazineSize.ToString();
            }
            else
            {
                text.text = bulletsLeft + " / " + magazineSize;
            }
        }

        void MyInput()
        {
            if (allowButtonHold) shooting = Input.GetKey(KeyCode.Mouse0);
            else shooting = Input.GetKeyDown(KeyCode.Mouse0);

            if (Input.GetKeyDown(KeyCode.R) && bulletsLeft < magazineSize && !reloading) Reload();

            if (readyToShoot && shooting && !reloading && bulletsLeft > 0)
            {
                bulletsShot = bulletsPerTap;
                Shoot();
            }
        }

        void Shoot()
        {
            if (bulletsLeft > 0)
            {
            bulletsLeft--;
            shootTimer = 0;

            float x = Random.Range(-spread, spread);
            float y = Random.Range(-spread, spread);

            Vector3 rayOrigin = Camera.main.ScreenPointToRay(Input.mousePosition).origin;
            Vector3 mousePosition = Input.mousePosition;
            Ray ray = Camera.main.ScreenPointToRay(mousePosition);
            Vector3 direction = ray.direction;

            if (Physics.Raycast(rayOrigin, direction, out rayHit, range))
            {
                if (rayHit.collider != null)
                {
                    if (rayHit.collider.CompareTag("Enemy"))
                    {
                        ShootingAi shootingAi = rayHit.collider.GetComponent<ShootingAi>();
                        if (shootingAi != null)
                        {
                            shootingAi.TakeDamage(damage);
                        }
                    }
                }
            }

        
            var bullet = Instantiate(bulletPrefab, gunBarrel.position, Quaternion.identity);
            var bulletRigidbody = bullet.GetComponent<Rigidbody>();
            bulletRigidbody.velocity = direction * bulletSpeed;

            bulletsShot--;

                if (bulletsShot > 0 && bulletsLeft > 0)
                {
                Invoke("Shoot", timeBetweenShots);
                }
                else if (bulletsLeft == 0)
                {
                Reload();
                }
            }
        }


        void ResetShot()
        {
            readyToShoot = true;
        }

        void Reload()
        {
            if (bulletsLeft < magazineSize)
            {
                reloading = true;
                Invoke("ReloadFinished", timeBetweenShooting);
            }
        }

        void ReloadFinished()
        {
            int neededBullets = magazineSize - bulletsLeft;
            int availableAmmo = Mathf.Min(neededBullets);
            bulletsLeft += availableAmmo;
            reloading = false;

        }
    }