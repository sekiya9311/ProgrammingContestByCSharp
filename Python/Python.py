import sys

N = int(input())

high = 2 * int(1e18)
low = 0

while high - low > 5:
    mid = (high + low) // 2
    if mid * (mid + 1) // 2 <= N:
        low = mid
    else:
        high = mid

for i in range(10):
    if (low + i) * (low + i + 1) // 2 == N:
        print("YES")
        print(low + i)
        sys.exit()
print("NO")
        
