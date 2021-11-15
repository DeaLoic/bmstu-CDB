# Database course project
BMSTU 2020-2021. 6th Semester IU7

### Launch:
cd Deploy <br>
docker-compose up

sudo fprobe  -iany -fip -B4096 -r2 -q10000 -t10000:10000000 0.0.0.0:2056


0.0.0.0:8234 - clickhouse <br>
0.0.0.0:2056 - netflow v5 <br>
0.0.0.0:2055 - netflow v7, v9